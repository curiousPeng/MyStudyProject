using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 模型映射配置
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    [System.Diagnostics.DebuggerNonUserCode]
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
}
