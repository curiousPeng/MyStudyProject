using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 实体映射访问者
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class EntityMappingVisitor<TModel> : ICellValueVisitor where TModel : new()
    {
        private readonly IMappingConfiguration<TModel> _entityMapping;
        private readonly SortedDictionary<int, IPropertyMapper<TModel>> _propertyMappers;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="entityMapping"></param>
        public EntityMappingVisitor(IMappingConfiguration<TModel> entityMapping)
        {
            if (entityMapping == null)
                throw new ArgumentNullException("entityMapping");

            _entityMapping = entityMapping;
            _propertyMappers = CreatePropertyMappers();
        }

        SortedDictionary<int, IPropertyMapper<TModel>> CreatePropertyMappers()
        {
            var propertyMappers = new SortedDictionary<int, IPropertyMapper<TModel>>();
            foreach (var propertyMapping in _entityMapping.GetPropertyMappings())
            {
                var expression = propertyMapping.PropertyExpression;
                if (expression.NodeType != ExpressionType.Lambda)
                    throw new InvalidOperationException("无效的属性访问表达式" + expression);

                propertyMappers[propertyMapping.ColumnIndex] = (IPropertyMapper<TModel>)Activator.CreateInstance(
                    typeof(PropertyMapper<,>).MakeGenericType(typeof(TModel), ((LambdaExpression)expression).ReturnType)
                    , expression);

            }
            return propertyMappers;
        }

        /// <summary>
        /// 设置或获取需要在访问时进行值绑定的对象
        /// </summary>
        public TModel Entity { get; set; }

        public void Visit(ICell cell)
        {
            if (Entity == null)
                throw new InvalidOperationException("在访问之前必须设置关联的Entity属性值.");

            // 无映射，跳过
            if (!_propertyMappers.ContainsKey(cell.ColumnIndex))
                return;

            _propertyMappers[cell.ColumnIndex].Map(cell, Entity);
        }
    }
}
