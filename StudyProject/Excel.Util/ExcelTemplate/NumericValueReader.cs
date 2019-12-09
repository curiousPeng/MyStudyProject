using System;
using NPOI.SS.UserModel;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 数值读取
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [System.Diagnostics.DebuggerNonUserCode]
    class NumericValueReader<TValue> : CellValueReader<TValue>
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
                    value = cell.BooleanCellValue ? 1 : 0;
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue;
                    break;
                default:
                    decimal v;
                    if (!decimal.TryParse(cell.StringCellValue, out v))
                        throw new InvalidOperationException($"值“{cell.StringCellValue}”无法转换为有效的数字。");
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
                throw new InvalidOperationException("空单元格无法转换为有效数字.");
            }

            return (TValue)Convert.ChangeType(value, typeof(TValue));
        }
    }
}
