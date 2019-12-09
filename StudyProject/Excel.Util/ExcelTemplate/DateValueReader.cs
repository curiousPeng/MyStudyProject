using System;
using NPOI.SS.UserModel;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 日期读取
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]
    class DateValueReader<TValue> : CellValueReader<TValue>
    {
        static readonly bool _nullableType = Nullable.GetUnderlyingType(typeof(TValue)) != null;
        static readonly Type _valueType = Nullable.GetUnderlyingType(typeof(TValue));

        protected override TValue ReadValue(ICell cell, CellType cellType)
        {
            object value;
            switch (cellType)
            {
                case CellType.Blank:
                    value = null;
                    break;
                case CellType.Boolean:
                    throw new InvalidOperationException("真假值无法转换为有效的日期。");
                case CellType.Numeric:
                    value = cell.DateCellValue;
                    break;
                default:
                    DateTime v;
                    if (!DateTime.TryParse(cell.StringCellValue, out v))
                        throw new InvalidOperationException($"值“{cell.StringCellValue}”无法转换为有效的日期。");
                    value = v;
                    break;
            }

            if (_nullableType)
            {
                if (value == null)
                    return default(TValue);

                return (TValue)Activator.CreateInstance(typeof(TValue), Convert.ChangeType(value, _valueType));
            }
            else if (value == null) // 非Nullable 遭遇空值
            {
                throw new InvalidOperationException("空单元格无法转换为有效的日期.");
            }

            return (TValue)Convert.ChangeType(value, typeof(TValue));
        }
    }
}
