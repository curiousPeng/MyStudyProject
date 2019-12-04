using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using NPOI.SS.UserModel;


namespace Excel.Util.ExcelTemplate
{
    public class PropertyMapper<TModel, TProperty> : IPropertyMapper<TModel>
    {
        private readonly MemberInfo _member;
        private readonly ICellValueReader<TProperty> _valueReader;
        private readonly Action<TModel, TProperty> _propertyMap;
        private readonly string _displayName;

        public PropertyMapper(Expression<Func<TModel, TProperty>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member == null)
                throw new InvalidOperationException("无效的属性访问表达式" + expression);

            _member = ((MemberExpression)expression.Body).Member;
            _valueReader = CellValueReader.Build<TProperty>();
            _displayName = ExtraceDisplayName(_member);
            _propertyMap = CreatePropertyMapAction(expression);
        }

        static string ExtraceDisplayName(MemberInfo member)
        {
            var display = (DisplayAttribute)Attribute.GetCustomAttribute(member, typeof(DisplayAttribute));
            return display?.Name ?? member.Name;
        }

        #region    IPropertyMapper<TModel>


        MemberInfo IPropertyMapper<TModel>.Member { get { return _member; } }

        void IPropertyMapper<TModel>.Map(ICell cell, TModel model)
        {
            try
            {
                var propertyValue = _valueReader.ReadValue(cell);
                _propertyMap(model, propertyValue);
            }
            catch (Exception e)
            {
                throw new Exception("读取 " + _displayName + " 失败, " + e.Message, e);
            }
        }
        #endregion

        static Action<TModel, TProperty> CreatePropertyMapAction(Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = (MemberExpression)expression.Body;
            var valueParameter = Expression.Parameter(typeof(TProperty), "value");

            return Expression.Lambda<Action<TModel, TProperty>>(
                        Expression.Assign(expression.Body, valueParameter),
                         expression.Parameters[0], valueParameter).Compile();
        }
    }
}
