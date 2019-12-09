using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;

namespace Excel.Util
{
    /// <summary>
    /// 单元格写入
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]
    public abstract class ExcelCellWriter : IExcelCellWriter
    {
        public void Write(ICell cell, object value)
        {
            if (cell == null)
                throw new ArgumentNullException("cell");
            // 如果是 null 值，则跳过此单元格写入
            if (value == null || value == DBNull.Value)
                return;

            OnWrite(cell, value);
        }

        protected abstract void OnWrite(ICell cell, object value);

        /// <summary>
        /// 创建写入器
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        static public IExcelCellWriter CreateWriter(Type valueType)
        {
            if (valueType == null)
                throw new ArgumentNullException("valueType");
             
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    return new BooleanCellWriter();
                case TypeCode.DateTime:
                    return new DateCellWriter();
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.SByte:
                case TypeCode.Single:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return new NumericCellWriter();
                case TypeCode.Char:
                case TypeCode.DBNull:
                case TypeCode.String:
                case TypeCode.Object:
                case TypeCode.Empty:
                default:
                    return new StringCellWriter();
            }
        }
    }
}
