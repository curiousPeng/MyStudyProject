using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 单元格值读取器
    /// </summary>
    /// <typeparam name="TValue">值类型</typeparam>
    public interface ICellValueReader<TValue>
    {
        /// <summary>
        /// 读取值
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="cell"></param>
        /// <returns></returns>
        TValue ReadValue(ICell cell);
    }
}
