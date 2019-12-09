using StudyProject.Example.Excel;
using System;

namespace StudyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO ,excel的导出demo
            var list = ExcelDemo.Inport();
            foreach(var item in list)
            {
                Console.WriteLine(item.itemName);
            }
            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}
