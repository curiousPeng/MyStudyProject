using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 模型映射配置
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IMappingConfiguration<TModel>
    {
        /// <summary>
        /// 按顺序列号映射指定属性
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="property">模型列表达式，如：_ => _.Code</param>
        /// <returns></returns>
        IMappingConfiguration<TModel> Map<TProperty>(Expression<Func<TModel, TProperty>> property);

        /// <summary>
        /// 将指定列映射到模型的指定属性上
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="columnIndex">列索引号，从0开始</param>
        /// <param name="property">模型列表达式，如：_ => _.Code</param>
        /// <returns>返回当前映射配置</returns>
        IMappingConfiguration<TModel> Map<TProperty>(int columnIndex, Expression<Func<TModel, TProperty>> property);

        /// <summary>
        /// 获取当前映射的属性数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 获取属性映射
        /// </summary>
        /// <returns></returns>
        IEnumerable<PropertyMapping<TModel>> GetPropertyMappings();
    }
}
