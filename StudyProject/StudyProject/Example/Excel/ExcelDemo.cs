using Excel.Util;
using Excel.Util.ExcelTemplate;
using StudyProject.Example.Excel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StudyProject.Example.Excel
{
    public class ExcelDemo
    {
        public static IList<InportModel> Inport()
        {
            var path = @"E:\Project\MyStudyProject\StudyProject\StudyProject\Example\Excel\test.xlsx";
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                var template = new ExcelTemplate<InportModel>(1);
                template.MapAllProperties();
                string ext = Path.GetExtension(path);
                Action<NPOI.SS.UserModel.IWorkbook> custom = null;
                NPOI.SS.UserModel.IWorkbook workbook;
                if (".xls".Equals(ext, StringComparison.CurrentCultureIgnoreCase))
                {
                    workbook = stream.AsXls();
                }
                else if (".xlsx".Equals(ext, StringComparison.CurrentCultureIgnoreCase))
                {
                    workbook = stream.AsXlsx();
                }
                else
                {
                    throw new InvalidOperationException("选择的文件类型不支持，请选择Excel数据文件.");
                }

                custom?.Invoke(workbook);
                workbook.Accept(template);

                template.Validate();    //校验

                return template.ToEntities();
            }
        }
    }
}
