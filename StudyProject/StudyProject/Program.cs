using StudyProject.Example.Excel;
using StudyProject.ThreadPool.Demo;
using System;

namespace StudyProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO ,excel的导入demo
            //var list = ExcelDemo.Inport();
            //foreach(var item in list)
            //{
            //    Console.WriteLine(item.itemName);
            //}
            CancellTaskDemo.RunDemo();
            Console.WriteLine("Print Enter exit Program!");
            Console.Read();
        }
    }
}
