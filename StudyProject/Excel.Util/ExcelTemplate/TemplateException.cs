using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Util.ExcelTemplate
{
    public class TemplateException : Exception
    {
        Lazy<string> columnName;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message"></param>
        /// <param name="row">行号</param>
        /// <param name="column">列号</param>
        public TemplateException(string message, int row, int column) : base(message)
        {
            columnName = new Lazy<string>(ColumnToName);
            Row = row;
            Column = column;
        }

        /// <summary>
        /// 行号（从0开始）
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 错误所在列号（从0开始）
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 列名，如A
        /// </summary>
        public string ColumnName { get { return columnName.Value; } }

        #region Methods
        static string ColumnToName(int column)
        {
            Stack<char> names = new Stack<char>();
            const int AlphaCount = 'Z' - 'A' + 1;
            do
            {
                names.Push((char)('A' + --column % AlphaCount));
                column /= AlphaCount;
            } while (column > 0);
            return string.Join("", names);
        }

        string ColumnToName()
        {
            return ColumnToName(Column + 1);
        }
        #endregion

        public override string Message
        {
            get
            {
                return $"第{Row + 1}行{ (Column >= 0 ? (ColumnName + "列") : null)}：" + base.Message;
            }
        }
    }
}
