using System;
using NPOI.SS.UserModel;

namespace Excel.Util
{
    class StringCellWriter : ExcelCellWriter
    {
        protected override void OnWrite(ICell cell, object value)
        {
            cell.SetCellValue((string)Convert.ChangeType(value, typeof(string)));
        }
    }
}
