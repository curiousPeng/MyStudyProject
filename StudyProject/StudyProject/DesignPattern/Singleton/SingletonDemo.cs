using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public class SingletonDemo
    {
        private static A _a;
        private SingletonDemo()
        {
        }
        static SingletonDemo()
        {
            _a = new A();
        }

        public static A GetA()
        {
            return _a;
        }
    }

    public class A
    {
        public int b { get; set; }
        public string c { get; set; }
    }
}
