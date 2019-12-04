using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace Excel.Util.ExcelTemplate
{
    /// <summary>
    /// Excel模板
    /// </summary>
    public static class WorkbookExtensions
    {
        /// <summary>
        /// 处理为 2003 Excel 文档
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        static public IWorkbook AsXls(this Stream stream)
        {
            try
            {
                return new HSSFWorkbook(stream);
            }
            catch (Exception)
            {
                throw new Exception("不是有效的excel文档内容.");
            }

        }

        /// <summary>
        /// 处理为 2007 Excel 文档
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        static public IWorkbook AsXlsx(this Stream stream)
        {
            return new XSSFWorkbook(stream);
        }

        /// <summary>
        /// 接收访问者执行
        /// </summary>
        /// <param name="workbook"></param>
        /// <param name="visitors"></param>
        static public void Accept(this IWorkbook workbook, params ICellValueVisitor[] visitors)
        {
            if (workbook == null)
                throw new NullReferenceException();

            workbook.GetSheetAt(0).Accept(visitors);
        }

        /// <summary>
        /// 接收访问者执行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="visitors"></param>
        static public void Accept(this ISheet sheet, params ICellValueVisitor[] visitors)
        {
            if (sheet == null)
                throw new NullReferenceException();

            if (visitors == null)
                throw new ArgumentNullException("visotors");

            var exceptions = new List<TemplateException>();

            var lastValidRowIndex = sheet.GetLastValidRowIndex();
            if (lastValidRowIndex < 0)
                throw new InvalidOperationException("数据文件无效.");

            var rows = sheet.GetRowEnumerator();

            while (rows.MoveNext()) // 遍历所有 行
            {
                var row = (IRow)rows.Current;
                if (row.RowNum > lastValidRowIndex) // 处理完最后一条数据，退出
                    break;

                var lastColumn = row.LastCellNum;
                for (var i = row.FirstCellNum; i < lastColumn; i++)
                {
                    var cell = row.GetCell(i);
                    foreach (var visitor in visitors)
                    {
                        try
                        {   // 当CELL为null时，创建空白单元格
                            visitor.Visit(cell ?? new HSSFCell(null, null, new BlankRecord { Column = i, Row = row.RowNum }));
                        }
                        catch (InvalidProgramException) { throw; }/*编程异常直接抛出*/
                        catch (Exception e)
                        {
                            exceptions.Add(new TemplateException(e.Message, row.RowNum, i));
                        }
                    } // ~visitors
                } // ~columns
            } // ~rows
            // 抛出聚合异常
            if (exceptions.Count > 0)
                throw new AggregateException("转换失败", exceptions);
        }

        /// <summary>
        /// 输出文件模板
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="template"></param>
        /// <param name="stream"></param>
        static public void OutputTemplateFile<TModel>(this ExcelTemplate<TModel> template, Stream stream, string sheetName = null) where TModel : new()
        {
            if (template == null)
                throw new NullReferenceException();

            if (stream == null)
                throw new ArgumentNullException("stream");

            IMappingConfiguration<TModel> mapping = template;
            var workbook = new HSSFWorkbook();
            try
            {
                var sheet = workbook.CreateSheet(sheetName ?? GetSheetNameFromType<TModel>());
                var row = sheet.CreateRow(0);
                var defaultStyle = workbook.CreateCellStyle();
                // 默认列样式，有边框线
                defaultStyle.BorderTop = BorderStyle.Thin;
                defaultStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                defaultStyle.BorderRight = BorderStyle.Thin;
                defaultStyle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                defaultStyle.BorderBottom = BorderStyle.Thin;
                defaultStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
                defaultStyle.BorderLeft = BorderStyle.Thin;
                defaultStyle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;

                // 标题样式：居中、加粗、有边框线
                var headerStyle = workbook.CreateCellStyle();
                headerStyle.CloneStyleFrom(defaultStyle);
                var headerFont = workbook.CreateFont();
                headerFont.Boldweight = 600;
                headerStyle.SetFont(headerFont);
                headerStyle.Alignment = HorizontalAlignment.Center;

                foreach (var property in mapping.GetPropertyMappings())
                {
                    sheet.SetDefaultColumnStyle(property.ColumnIndex, defaultStyle);

                    var column = row.CreateCell(property.ColumnIndex, CellType.String);
                    column.SetCellValue(GetColumnNameFromProperty(property.PropertyExpression));
                    column.CellStyle = headerStyle;
                    sheet.AutoSizeColumn(property.ColumnIndex);
                }
                sheet.CreateFreezePane(0, 1);
                workbook.Write(stream);
            }
            finally
            {
                workbook.Close();
            }
        }

        static string GetSheetNameFromType<TModel>()
        {
            var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(typeof(TModel), typeof(DescriptionAttribute));
            return descriptionAttribute == null ? typeof(TModel).Name : descriptionAttribute.Description;
        }

        static string GetColumnNameFromProperty(Expression property)
        {
            var member = ((MemberExpression)((LambdaExpression)property).Body).Member;
            var display = (DisplayAttribute)Attribute.GetCustomAttribute(member, typeof(DisplayAttribute));

            return display != null ? display.Name : member.Name;
        }

        /// <summary>
        /// 获取最后一行有数据的行索引号
        /// </summary>
        /// <returns></returns>
        static int GetLastValidRowIndex(this ISheet sheet)
        {
            var validRowIndex = sheet.LastRowNum;
            while (validRowIndex >= 0)
            {
                if (!sheet.GetRow(validRowIndex).IsNullOrEmpty())
                    return validRowIndex;

                validRowIndex--;
            }
            return -1;
        }

        static bool IsNullOrEmpty(this IRow row)
        {
            if (row != null && row.Cells != null && row.Cells.Count > 0)
            {
                foreach (var cell in row.Cells)
                {
                    if (cell == null)
                        continue;
                    var cellType = cell.CellType == CellType.Formula ? cell.CachedFormulaResultType : cell.CellType;

                    switch (cellType)
                    {
                        case CellType.Boolean:
                        case CellType.Numeric:
                            return false;
                        case CellType.String:
                            if (!string.IsNullOrWhiteSpace(cell.StringCellValue))
                                return false;
                            break;
                    }
                }
            }
            return true;
        }
    }
}
