using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 单元格值
    /// </summary>
    public interface ICellValueVisitor
    {
        /// <summary>
        /// 接受该单元格访问
        /// </summary>
        /// <param name="cell"></param>
        void Visit(ICell cell);
    }
}
