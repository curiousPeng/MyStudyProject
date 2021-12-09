using Excel.Util;
using Excel.Util.ExcelTemplate;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;

namespace ErpErrorHandle.Handle
{
    public static class ExcelHandel
    {
        public static List<T> GetExcelData<T>(string path) where T : new()
        {
            //var path = @"E:\Project\MyStudyProject\StudyProject\StudyProject\Example\Excel\test.xlsx";

            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                var template = new ExcelTemplate<T>(1);
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
                    throw new InvalidOperationException("文件类型的不支持，请选择Excel数据文件.");
                }

                custom?.Invoke(workbook);
                workbook.Accept(template);

                template.Validate();    //校验

                return template.ToEntities().ToList();
            }

        }
        /// <summary>
        /// 复制模板并写入数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourcePath">模板位置</param>
        /// <param name="path">存放的位置</param>
        /// <param name="data">数据</param>
        public static void CreateExcel<T>(string sourcePath,string path, List<T> data) where T : new()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            try
            {
                var newName = DateTime.Now.ToString("yyyyMMddHHmmss");
                //var templatePath = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Template\\template.xlsx");
                string strFilePath = CopyToFile(sourcePath, path, newName);
                var dt = data.ToDataTable();
                using (FileStream fs = new FileStream(strFilePath, FileMode.Open))
                {
                    IWorkbook workbook = new XSSFWorkbook(fs);
                    var sheet1 = workbook.GetSheetAt(0);
                    int num = sheet1.LastRowNum + 1;//获取最大行数

                    using (FileStream fout = new FileStream(strFilePath, FileMode.Open, FileAccess.Write, FileShare.ReadWrite)) //写入流
                    {
                        //获取到已存在的sheet
                        var sheet = workbook.GetSheetAt(0);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //创建行数时+num
                            var row = sheet.CreateRow(i + num);
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                ICell cell = row.CreateCell(j);
                                cell.SetCellValue(dt.Rows[i][j].ToString());
                            }
                        }
                        workbook.Write(fout);
                        fout.Close();
                    }
                    fs.Close();
                }
                Console.WriteLine("文件位置在：{0}", strFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("生成出现异常");
            }
        }

        /// <summary>
        /// 拷贝文件到另一个文件夹下
        /// </summary>
        /// <param name="sourceName">源文件路径</param>
        /// <param name="folderPath">目标路径,目标文件夹</param>
        public static string CopyToFile(string sourceName, string folderPath, string newFileName = "")
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //文件不用新的文件名，就用原文件文件名
            string fileName = Path.GetFileName(sourceName);
            if (!string.IsNullOrEmpty(newFileName))
            {
                fileName = newFileName + Path.GetExtension(sourceName);
            }
            //目标整体路径
            string targetPath = Path.Combine(folderPath, fileName);

            //Copy到新文件下
            FileInfo file = new FileInfo(sourceName);
            if (file.Exists)
            {
                //获得该文件的访问权限
                System.Security.AccessControl.FileSecurity fileSecurity = file.GetAccessControl();
                //添加ereryone用户组的访问权限规则 完全控制权限
                fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                //添加Users用户组的访问权限规则 完全控制权限
                fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                //设置访问权限
                file.SetAccessControl(fileSecurity);
                //true 覆盖已存在的同名文件，false不覆盖
                file.CopyTo(targetPath, true);
            }
            else
            {
                throw new FileNotFoundException("未找到文件");
            }
            return targetPath;
        }
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {

            //创建属性的集合  
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口  

            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列  
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例  
                DataRow row = dt.NewRow();
                //给row 赋值  
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable  
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
