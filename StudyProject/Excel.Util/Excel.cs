using System;
using System.IO;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;

namespace Excel.Util
{
    /// <summary>
    /// Excel数据导出类
    /// </summary>
    public class Excel : HSSFWorkbook
    {
        private readonly ICellStyle defaultCellStyle;
        private readonly ISheet defaultSheet;
        private readonly Action<Excel> customize;
        private readonly Dictionary<Tuple<Type, HorizontalAlignment?>, ICellStyle> styles = new Dictionary<Tuple<Type, HorizontalAlignment?>, ICellStyle>();

        /// <summary>
        /// 
        /// </summary>
        public ICellStyle DefaultCellStyle { get { return defaultCellStyle; } }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="sheetName">初始Sheet页名称`</param>
        public Excel(string sheetName, Action<Excel> customize)
        {
            defaultCellStyle = CreateCellStyle();

            defaultCellStyle.BorderTop = BorderStyle.Thin;
            defaultCellStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            defaultCellStyle.BorderRight = BorderStyle.Thin;
            defaultCellStyle.RightBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            defaultCellStyle.BorderBottom = BorderStyle.Thin;
            defaultCellStyle.TopBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            defaultCellStyle.BorderLeft = BorderStyle.Thin;
            defaultCellStyle.LeftBorderColor = NPOI.HSSF.Util.HSSFColor.Black.Index;
            defaultCellStyle.VerticalAlignment = VerticalAlignment.Center;

            var cellFont = CreateFont();
            cellFont.FontHeightInPoints = 11;
            cellFont.FontName = "宋体";
            defaultCellStyle.SetFont(cellFont);

            defaultSheet = CreateSheet(sheetName);

            this.customize = customize;
        }

        private void CreateSummaries()
        {
            var dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "中国石油天然气运输公司";

            DocumentSummaryInformation = dsi;

            var si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = defaultSheet.SheetName;
            si.Author = "运输管理系统";
            SummaryInformation = si;
        }

        /// <summary>
        /// 导出一个面签的数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="data"></param>
        public void ExportSheet(ISheet sheet, Sheet data)
        {
            // 输出标题列
            var head = sheet.CreateRow(0);
            var headFont = CreateFont();
            headFont.FontName = "宋体";
            headFont.FontHeightInPoints = 11;
            headFont.Boldweight = 600;
            var headCellStyle = CreateCellStyle();
            headCellStyle.CloneStyleFrom(defaultCellStyle);
            headCellStyle.Alignment = HorizontalAlignment.Center;
            headCellStyle.VerticalAlignment = VerticalAlignment.Center;
            headCellStyle.SetFont(headFont);
            var columns = data.Columns;
            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                SetColumnDefaultStyle(sheet, i, column);
                var cell = head.CreateCell(i);
                cell.SetCellValue(column.ColumnName);
                cell.CellStyle = headCellStyle;
            }

            // 输出行
            var rowNumber = 1;
            var rowWriter = new ExcelRowWriter(columns);
            foreach (var rowValues in data.Rows)
            {
                var row = sheet.CreateRow(rowNumber++);
                rowWriter.Write(row, rowValues);
                if (rowNumber == 100) // 采样到100时自适应大小
                    for (int i = 0; i < columns.Length; i++)
                    {
                        sheet.AutoSizeColumn(i);
                        sheet.SetColumnWidth(i, Math.Min(sheet.GetColumnWidth(i) + 1000, 65280));
                    }
            }

            // 自适应列宽，弥补小于100行的情况
            for (int i = 0; rowNumber < 100 && i < columns.Length; i++)
            {
                sheet.AutoSizeColumn(i);
                sheet.SetColumnWidth(i, Math.Min(sheet.GetColumnWidth(i) + 1000, 65280));
            }
            sheet.CreateFreezePane(0, 1);
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sheetData"></param>
        /// <param name="outputStream"></param>
        public void Export(Sheet sheetData, Stream outputStream)
        {
            ExportSheet(defaultSheet, sheetData);

            CreateSummaries();

            // 若传递了自定义，则调用自定义处理
            customize?.Invoke(this);

            Write(outputStream);
        }

        /// <summary>
        /// 设置列样式，默认带边框
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        /// <param name="columnName"></param>
        /// <param name="columnType"></param>
        public void SetColumnDefaultStyle(ISheet sheet, int columnIndex, Type columnType = null, HorizontalAlignment? align = null)
        {
            if (sheet == null)
                throw new ArgumentNullException("sheet");
            if (columnIndex < 0)
                throw new IndexOutOfRangeException("columnIndex");
            var previousColumnStyle = sheet.GetColumnStyle(columnIndex);

            var styleKey = Tuple.Create(columnType, align);
            ICellStyle defaultColumnStyle;
            if (!styles.TryGetValue(styleKey, out defaultColumnStyle))
            {
                defaultColumnStyle = CreateCellStyle();
                defaultColumnStyle.CloneStyleFrom(defaultCellStyle);
                if (columnType != null && columnType.IsValueType)
                {
                    if (columnType == typeof(DateTime))
                    {
                        defaultColumnStyle.DataFormat = (short)Workbook.CreateFormat("yyyy-MM-dd");
                    }
                    defaultColumnStyle.Alignment = HorizontalAlignment.Right;
                }
                else if (columnType == typeof(string))
                {
                    defaultColumnStyle.DataFormat = HSSFDataFormat.GetBuiltinFormat("@");
                }
                if (align.HasValue)
                    switch (align)
                    {
                        case HorizontalAlignment.Center:
                            defaultColumnStyle.Alignment = HorizontalAlignment.Center;
                            break;
                        case HorizontalAlignment.Left:
                            defaultColumnStyle.Alignment = HorizontalAlignment.Left;
                            break;
                        case HorizontalAlignment.Right:
                            defaultColumnStyle.Alignment = HorizontalAlignment.Right;
                            break;
                    }
                styles[styleKey] = defaultColumnStyle;
            }

            sheet.SetDefaultColumnStyle(columnIndex, defaultColumnStyle);

            // 刷新已知样式
            if (previousColumnStyle != null)
            {
                for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                {
                    var cell = sheet.GetRow(i).GetCell(columnIndex);
                    if (cell != null && Equals(cell.CellStyle, previousColumnStyle))
                        cell.CellStyle = defaultColumnStyle;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="styling"></param>
        public void SetColumnStyle(int columnIndex, Action<ICellStyle> styling, int sheetIndex = 0)
        {
            var sheet = GetSheetAt(sheetIndex);
            var style = CreateCellStyle();
            var previousStyle = sheet.GetColumnStyle(columnIndex);
            style.CloneStyleFrom(previousStyle);
            styling(style);
            sheet.SetDefaultColumnStyle(columnIndex, style);
            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
            {
                var cell = sheet.GetRow(i).GetCell(columnIndex);
                if (cell != null && Equals(cell.CellStyle, previousStyle))
                    cell.CellStyle = style;
            }
        }

        /// <summary>
        /// 设置列样式，默认带边框
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="columnIndex"></param>
        /// <param name="column"></param>
        public void SetColumnDefaultStyle(ISheet sheet, int columnIndex, ExcelColumn column)
        {
            if (sheet == null)
                throw new ArgumentNullException("sheet");
            if (columnIndex < 0)
                throw new IndexOutOfRangeException("columnIndex");
            if (column == null)
                throw new ArgumentNullException("column");

            SetColumnDefaultStyle(sheet, columnIndex, column.ColumnType);
        }
    }
}
