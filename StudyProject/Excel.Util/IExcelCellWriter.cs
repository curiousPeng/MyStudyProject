using NPOI.SS.UserModel;

namespace Excel.Util
{
    /// <summary>
    /// 单元格值写入器
    /// </summary>
    public interface IExcelCellWriter
    {
        /// <summary>
        /// 向指定单元格写入值 
        /// </summary>
        /// <param name="cell">目标单元格</param>
        /// <param name="value">值</param>
        void Write(ICell cell, object value);
    }
}
