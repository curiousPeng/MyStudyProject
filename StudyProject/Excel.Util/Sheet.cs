using System;
using System.Collections.Generic;
using System.Linq;

namespace Excel.Util
{
    /// <summary>
    /// Sheet页
    /// </summary>
    public class Sheet
    {
        /// <summary>
        /// 列定义
        /// </summary>
        public ExcelColumn[] Columns { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public IList<object[]> Rows { get; set; }

        /// <summary>
        /// 重全名标题列名
        /// </summary>
        /// <param name="oldName">原名称</param>
        /// <param name="newName">新名称</param>
        /// <returns></returns>
        public Sheet RenameColumn(string oldName, string newName)
        {
            if (oldName == null)
                throw new ArgumentNullException("oldName");

            var column = Columns?.FirstOrDefault(_ => string.Compare(oldName, _.ColumnName, true) == 0);
            if (column == null)
                throw new InvalidOperationException("未发现源列：" + oldName);
            column.ColumnName = newName ?? string.Empty;

            return this;
        }
    }
}
