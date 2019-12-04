using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Util
{
    /// <summary>
    /// Excel单元格
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ColumnName}/{ColumnType.Name}")]
    public class ExcelColumn
    {
        /// <summary>
        /// 默认构造
        /// </summary>
        public ExcelColumn() { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columnType"></param>
        public ExcelColumn(string columnName, Type columnType)
        {
            ColumnName = columnName;
            ColumnType = columnType;
        }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 列数据类型
        /// </summary>
        public Type ColumnType { get; set; }
    }
}
