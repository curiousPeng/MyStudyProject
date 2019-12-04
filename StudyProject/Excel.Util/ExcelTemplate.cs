using Excel.Util.ExcelTemplate;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Excel.Util
{
    /// <summary>
    /// Excel模板
    /// </summary>
    public class ExcelTemplate<TModel> : ICellValueVisitor, IMappingConfiguration<TModel> where TModel : new()
    {
        #region Field & Constructor
        private Lazy<EntityMappingVisitor<TModel>> _visotor;
        /// <summary>
        /// 行模型  Key 行索引  Value 实体数据对象，结果按行索引排序
        /// </summary>
        private SortedDictionary<int, TModel> _rowModels = new SortedDictionary<int, TModel>();

        private MappingConfiguration<TModel> _mapping = new MappingConfiguration<TModel>();
        private Dictionary<string, int> _propertyColumnMapping = new Dictionary<string, int>();

        private readonly int _headerRows;
        /// <summary>
        /// 当前行
        /// </summary>
        private int _currentRow = int.MinValue;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="headerRows">列头所占行数</param>
        public ExcelTemplate(int headerRows = 1)
        {
            _headerRows = headerRows;
            _visotor = new Lazy<EntityMappingVisitor<TModel>>(BuildVisitor);
        }

        #endregion

        #region Protected Members
        /// <summary>
        /// 获取当前行的数据模型
        /// </summary>
        /// <returns></returns>
        protected TModel CurrentRow()
        {
            if (!_visotor.IsValueCreated)
                throw new InvalidOperationException("必须在访问阶段访问当前行模型.");

            return _visotor.Value.Entity;
        }

        /// <summary>
        /// 获取指定属性存放的索引位置
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        protected int GetPropertyColumnIndex(string propertyName)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            return _propertyColumnMapping[propertyName];
        }

        /// <summary>
        /// 获取指定属性存放的索引位置
        /// </summary>
        /// <param name="property">属性表达式，如 _ => _.Code</param>
        /// <returns></returns>
        protected int GetPropertyColumnIndex<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            if (property == null)
                throw new ArgumentNullException("property");
            if (!(property.Body is MemberExpression))
                throw new InvalidOperationException("property 仅能访问成员，如 _ => _.Code");

            return GetPropertyColumnIndex(((MemberExpression)property.Body).Member.Name);
        }
        /// <summary>
        /// 访问
        /// </summary>
        /// <param name="cell"></param>
        protected virtual void OnVisit(ICell cell)
        {
            if (_currentRow != cell.RowIndex)
            {
                _currentRow = cell.RowIndex;
                var model = new TModel();
                _visotor.Value.Entity = model;
                _rowModels[_currentRow] = model;
            }
            _visotor.Value.Visit(cell);
        }

        /// <summary>
        /// 自定义行验证，默认为空
        /// </summary>
        /// <param name="value"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        protected virtual IEnumerable<TemplateException> CustomValidate(TModel value, int rowIndex)
        {
            return Enumerable.Empty<TemplateException>();
        }
        #endregion

        #region Private Methods
        EntityMappingVisitor<TModel> BuildVisitor()
        {
            if (_propertyColumnMapping.Count == 0)
                throw new InvalidProgramException("未给模板映射任何列，请先调用映射： template.Map(0, _=> _.Code) 或 tempalte.MapAllProperties()");

            return new EntityMappingVisitor<TModel>(this);
        }
        /// <summary>
        /// 添加属性映射
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="expression"></param>
        /// <param name="columnIndex"></param>
        void AddPropertyColumnMapping<TProperty>(Expression<Func<TModel, TProperty>> expression, int columnIndex)
        {
            if (!(expression.Body is MemberExpression))
                throw new InvalidOperationException("仅支持属性成员表达式, 如 _=> _.Code");
            _propertyColumnMapping[((MemberExpression)expression.Body).Member.Name] = columnIndex;
        }

        TemplateException CreateExceptionFromValidation(ValidationResult validationResult, int row)
        {
            if (validationResult.MemberNames == null)
                return null;

            int column;
            if (!_propertyColumnMapping.TryGetValue(validationResult.MemberNames.FirstOrDefault(), out column))
                return null;

            return new TemplateException(validationResult.ErrorMessage, row, column);
        }

        #endregion

        #region IMappingConfiguration
        int IMappingConfiguration<TModel>.Count { get { return _mapping.Count; } }

        IEnumerable<PropertyMapping<TModel>> IMappingConfiguration<TModel>.GetPropertyMappings()
        {
            return _mapping.GetPropertyMappings();
        }

        #endregion

        #region ICellValueVisitor
        void ICellValueVisitor.Visit(ICell cell)
        {
            if (cell.RowIndex < _headerRows)   // 仅处理标题行后续内容
                return;

            OnVisit(cell);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// 映射属性
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        public IMappingConfiguration<TModel> Map<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            if (_visotor.IsValueCreated)
                throw new InvalidOperationException("模板已使用后，不允许再进行映射配置.");

            AddPropertyColumnMapping(property, _mapping.Count);
            _mapping.Map(property);
            return this;
        }

        /// <summary>
        /// 映射属性到指定列
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="columnIndex"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public IMappingConfiguration<TModel> Map<TProperty>(int columnIndex, Expression<Func<TModel, TProperty>> property)
        {
            if (_visotor.IsValueCreated)
                throw new InvalidOperationException("模板已使用后，不允许再进行映射配置.");

            AddPropertyColumnMapping(property, columnIndex);
            _mapping.Map(columnIndex, property);
            return this;
        }

        /// <summary>
        /// 模板数据转实体模型数据
        /// </summary>
        /// <returns></returns>
        public IList<TModel> ToEntities()
        {
            return _rowModels.Values.ToArray();
        }

        /// <summary>
        /// 执行数据校验
        /// </summary>
        public void Validate()
        {
            var validationResults = new List<ValidationResult>();
            var templateExceptions = new List<TemplateException>();

            foreach (var rowModel in _rowModels)
            {
                validationResults.Clear();
                var validationContext = new ValidationContext(rowModel.Value, null, null);
                if (!Validator.TryValidateObject(rowModel.Value, validationContext, validationResults, true))
                {
                    foreach (var validationResult in validationResults)
                    {
                        var templateException = CreateExceptionFromValidation(validationResult, rowModel.Key);
                        if (templateException == null)
                        {
                            System.Diagnostics.Trace.WriteLine("非导入列引起的异常，忽略：" + validationResult.ErrorMessage);
                        }
                        else
                        {
                            templateExceptions.Add(templateException);
                        }
                    }
                }
                // 调用自定义验证
                templateExceptions.AddRange(CustomValidate(rowModel.Value, rowModel.Key));
            }
            if (templateExceptions.Count > 0)
            {
                throw new AggregateException("验证失败.", templateExceptions);
            }
        }
        #endregion
    }
}
