using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 单元格值读取器
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public abstract class CellValueReader<TValue> : ICellValueReader<TValue>
    {
        /// <summary>
        /// 读取值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public TValue ReadValue(ICell cell)
        {
            CellType cellType;
            if (cell.CellType == CellType.Formula)  // 公式取结果类型
            {
                cellType = cell.CachedFormulaResultType;
            }
            else
            {
                cellType = cell.CellType;
            }

            if (!ValidateCellType(cellType))        // 校验失败
            {
                throw new InvalidOperationException("单元格值无效.");
            }

            return ReadValue(cell, cellType);
        }

        /// <summary>
        /// 校验单元格类型
        ///     默认 ERROR\UNKNOWN时失败
        /// </summary>
        /// <param name="cellType"></param>
        /// <returns></returns>
        protected virtual bool ValidateCellType(CellType cellType)
        {
            return cellType != CellType.Unknown && cellType != CellType.Error;
        }

        /// <summary>
        /// 读取值
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="cellType">单元格值类型，如果单元格是公式，则该值为公式结果类型</param>
        /// <returns></returns>
        protected abstract TValue ReadValue(ICell cell, CellType cellType);
    }
}

