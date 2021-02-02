using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Features
{
    public class InitDemo
    {
        public void InitSetter()
        {
            var a = new A() { age=1,Name="aa",Phone="13211111111"};
            // a.age = 2;
            // a.Name = "bbb";
            a.Phone = "13111111111";
        }
    }

    public class A
    {
        public string Name { get; init; }
        public int age { get; init; }
        public string Phone{get;set;}
    }
}
