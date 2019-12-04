using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 属性映射
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IPropertyMapper<TModel>
    {
        /// <summary>
        /// 获取该属性映射对应的成员
        /// </summary>
        MemberInfo Member { get; }
        /// <summary>
        /// 将 cell 单元格的值，映射到 model 中指定的属性中
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="model"></param>
        void Map(ICell cell, TModel model);
    }
}
