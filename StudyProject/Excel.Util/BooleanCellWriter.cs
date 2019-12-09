using NPOI.SS.UserModel;

namespace Excel.Util
{
    class BooleanCellWriter : ExcelCellWriter
    {
        protected override void OnWrite(ICell cell, object value)
        {
            cell.SetCellValue((bool)value);
        }
    }
}
