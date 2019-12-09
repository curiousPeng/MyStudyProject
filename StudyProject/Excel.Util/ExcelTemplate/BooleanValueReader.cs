using System;
using NPOI.SS.UserModel;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// 真假值读取
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [System.Diagnostics.DebuggerNonUserCode]
    class BooleanValueReader<TValue> : CellValueReader<TValue>
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
                    value = cell.BooleanCellValue;
                    break;
                case CellType.Numeric:
                    value = cell.NumericCellValue != 0;/*非0值*/
                    break;
                default:
                    bool v;
                    if (!bool.TryParse(cell.StringCellValue, out v))
                    {
                        switch (cell.StringCellValue.Trim())
                        {
                            case "是":
                                value = true;
                                break;
                            case "":
                            case " ":
                            case "否":
                                value = false;
                                break;
                            default:
                                throw new InvalidOperationException($"值“{cell.StringCellValue}”无法转换为有效的真假值。");
                        }
                    }
                    else
                    {
                        value = v;
                    }
                    break;
            }

            if (value == null)
                return default(TValue);
            else if (_nullableType)
            {
                return (TValue)Activator.CreateInstance(typeof(TValue), Convert.ChangeType(value, _valueType));
            }

            return (TValue)Convert.ChangeType(value, typeof(TValue));
        }
    }

    /// <summary>
    /// 值读取器
    /// </summary>
    static public class CellValueReader
    {
        /// <summary>
        /// 创建值读取器
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        static public ICellValueReader<TValue> Build<TValue>()
        {
            var valueType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    return new BooleanValueReader<TValue>();
                case TypeCode.Byte:
                case TypeCode.Char:
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
                    return new NumericValueReader<TValue>();
                case TypeCode.String:
                    return (ICellValueReader<TValue>)new StringValueReader();
                case TypeCode.DateTime:
                    return new DateValueReader<TValue>();
                default:
                    throw new NotSupportedException("不支持的值类型读取 " + typeof(TValue));
            }
        }
    }
}
