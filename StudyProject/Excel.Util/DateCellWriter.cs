using System;
using NPOI.SS.UserModel;

namespace Excel.Util
{
    class DateCellWriter : ExcelCellWriter
    {
        protected override void OnWrite(ICell cell, object value)
        {
            if (DateTime.MinValue != (DateTime)value) // 是小日期忽略
                cell.SetCellValue((DateTime)value);
        }
    }
}
