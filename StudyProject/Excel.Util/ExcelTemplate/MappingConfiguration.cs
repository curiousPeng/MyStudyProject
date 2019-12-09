using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 模型映射配置
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    class MappingConfiguration<TModel> : IMappingConfiguration<TModel>
    {
        SortedDictionary<int, Expression> _propertyMappings = new SortedDictionary<int, Expression>();

        public int Count { get { return _propertyMappings.Count; } }

        public IEnumerable<PropertyMapping<TModel>> GetPropertyMappings()
        {
            foreach (var mapping in _propertyMappings)
                yield return new PropertyMapping<TModel>(mapping.Key, mapping.Value);
        }

        public IMappingConfiguration<TModel> Map<TProperty>(Expression<Func<TModel, TProperty>> property)
        {
            _propertyMappings[Count] = property;
            return this;
        }

        public IMappingConfiguration<TModel> Map<TProperty>(int columnIndex, Expression<Func<TModel, TProperty>> property)
        {
            if (columnIndex < 0 || columnIndex > 256)
                throw new IndexOutOfRangeException("columnIndex只能为0-256的值.");

            _propertyMappings[columnIndex] = property;
            return this;
        }
    }
    /// <summary>
    /// 映射配置工具类
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]

    public static class MappingConfiguration
    {
        /// <summary>
        /// 初始化指定模型的映射配置
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        static public IMappingConfiguration<TModel> For<TModel>()
        {
            return new MappingConfiguration<TModel>();
        }

        /// <summary>
        /// 批量映射
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TRange"></typeparam>
        /// <param name="mappingConfiguration"></param>
        /// <param name="rangeProperties">范围属性表达式，如 _ => new { _.Code, _.Name }</param>
        /// <returns></returns>
        static public IMappingConfiguration<TModel> MapRange<TModel, TRange>(this IMappingConfiguration<TModel> mappingConfiguration, Expression<Func<TModel, TRange>> rangeProperties)
        {
            if (mappingConfiguration == null)
                throw new NullReferenceException();

            if (rangeProperties == null)
                throw new ArgumentNullException("rangeProperties");


            if (!(rangeProperties.Body is NewExpression))
                throw new InvalidOperationException("complex 仅支持 _ => new { _.Member1, ... }这样的表达式");

            var parameter = rangeProperties.Parameters[0];

            foreach (var expression in ((NewExpression)rangeProperties.Body).Arguments)
            {
                Activator.CreateInstance(typeof(PropertyMapAction<,>).MakeGenericType(typeof(TModel), expression.Type)
                    , mappingConfiguration, parameter, expression);
            }
            return mappingConfiguration;
        }

        /// <summary>
        /// 映射当前模型的所有属性
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="mappingConfiguration"></param>
        static public void MapAllProperties<TModel>(this IMappingConfiguration<TModel> mappingConfiguration)
        {
            if (mappingConfiguration == null)
                throw new NullReferenceException();

            var parameter = Expression.Parameter(typeof(TModel), "_");
            foreach (var property in typeof(TModel).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var body = Expression.MakeMemberAccess(parameter, property);
                Activator.CreateInstance(typeof(PropertyMapAction<,>).MakeGenericType(typeof(TModel), property.PropertyType)
                    , mappingConfiguration, parameter, body);
            }
        }

        /// <summary>
        /// 属性映射
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        [System.Diagnostics.DebuggerNonUserCode]
        class PropertyMapAction<TModel, TProperty>
        {
            public PropertyMapAction(IMappingConfiguration<TModel> mappingConfiguration, ParameterExpression parameter, Expression body)
            {
                mappingConfiguration.Map(Expression.Lambda<Func<TModel, TProperty>>(body, parameter));
            }
        }
    }
}
