using System;
using NPOI.SS.UserModel;

namespace Excel.Util
{
    class NumericCellWriter : ExcelCellWriter
    {
        protected override void OnWrite(ICell cell, object value)
        {
            // (double)0.96f  BUG FIXED，先将 value 转字符串再转double
            cell.SetCellValue(Convert.ToDouble(value.ToString()));
        }
    }
}
