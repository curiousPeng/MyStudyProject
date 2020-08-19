using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.CSharpBasic
{
    public class StringDemo
    {
        public void Demo()
        {
            var p1 = new DemoString { first = "peng", last = "curious" };
            Console.WriteLine(p1.ToString("F"));//打印peng
            Console.WriteLine(p1.ToString());//打印curious peng
            Console.WriteLine(p1.ToString("L"));//打印curious
            Console.WriteLine($"{p1:F}");//打印curious
            Console.WriteLine(p1.ToString("C"));//这里会抛异常了
        }
    }
    public class DemoString : IFormattable
    {
        public string first { get; set; }
        public string last { get; set; }
        public override string ToString()
        {
            return  this.last + " " + this.first;
        }

        public virtual string ToString(string format) => ToString(format, null);
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case null:
                case "A":
                    return ToString();
                case "F":
                    return first;
                case "L":
                    return last;
                default:
                    throw new FormatException($"invalid format string {format}");
            }
        }
    }
}
