
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern.Prototype
{
    /// <summary>
    /// 原型模式
    /// </summary>
    class PrototypeDemo
    {
        public void demo()
        {
            var o = new A();
            o.a = "111";
            var b = o.Clone() as A;
            b.a = "222";
            var j = o.Clone() as A;
            j.a = "333";

        }
    }

    public class A : ICloneable
    {
        public string a;

        public object Clone()
        {
            A obj = new A();
            obj.a = this.a;
            return obj;
        }
    }
}
