using NPOI.SS.UserModel;

namespace Excel.Util
{
    /// <summary>
    /// Excel行写入
    /// </summary>
    public interface IExcelRowWriter
    {
        /// <summary>
        /// 往指定行写入值
        /// </summary>
        /// <param name="row">目标行</param>
        /// <param name="values">要写入的值</param>
        void Write(IRow row, object[] values);
    }
}
