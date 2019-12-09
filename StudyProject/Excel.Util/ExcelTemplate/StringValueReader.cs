using NPOI.SS.UserModel;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 字符串
    ///     注：会自动处理末端空白字符
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]
    class StringValueReader : CellValueReader<string>
    {
        static readonly DataFormatter formatter = new DataFormatter();
        protected override string ReadValue(ICell cell, CellType cellType)
        {
            switch (cellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue ? "是" : "否";
                case CellType.Numeric:
                    if (cell.CellStyle.DataFormat > 0)
                        return formatter.FormatCellValue(cell);
                    return cell.NumericCellValue.ToString();
                default:
                    return cell.StringCellValue.TrimEnd();  // 自动去除末端空白
            }
        }
    }
}
