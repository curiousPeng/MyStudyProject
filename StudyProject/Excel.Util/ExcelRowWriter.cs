using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;

namespace Excel.Util
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    ///  var rowWriter = new ExcelRowWriter(columns);
    ///  int rowNumber = 1;
    ///  foreach(var row in sheetData.Rows) {
    ///     IRow row = sheet.CreateRow(rowNumber ++);
    ///     rowWriter(row, values);
    ///  }
    /// </example>
    [System.Diagnostics.DebuggerNonUserCode]
    public class ExcelRowWriter : IExcelRowWriter
    {
        private readonly ExcelColumn[] columns;
        private Lazy<IExcelCellWriter[]> columnWriters;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="columns">列定义</param>
        public ExcelRowWriter(ExcelColumn[] columns)
        {
            this.columns = columns;
            columnWriters = new Lazy<IExcelCellWriter[]>(CreateColumnWriter);
        }

        public void Write(IRow row, object[] values)
        {
            for (int i = 0; i < columnWriters.Value.Length && i < values.Length; i++)
            {
                var cell = row.CreateCell(i);
                columnWriters.Value[i].Write(cell, values[i]);
            }
        }

        private IExcelCellWriter[] CreateColumnWriter()
        {
            var writers = new IExcelCellWriter[columns.Length];
            for (int i = 0; i < writers.Length; i++)
            {
                writers[i] = ExcelCellWriter.CreateWriter(columns[i].ColumnType ?? typeof(string));
            }
            return writers;
        }
    }
}
