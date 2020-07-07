
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern.Prototype
{
    /// <summary>
    /// 原型模式,所谓的原型模式难道就是深拷贝？
    /// </summary>
    public class PrototypeDemo
    {
        public void demo()
        {
            var o = new A();
            o.a = "111";
            var b = o.Clone() as A;
            b.a = "222";
            var j = o.Clone() as A;
            j.a = "333";
            Console.WriteLine(o.a);
            Console.WriteLine(b.a);
            Console.WriteLine(j.a);
        }
    }

    public class A : IA, ICloneable
    {
        public string a;

        public object Clone()
        {
            A obj = new A();
            obj.a = this.a;
            return obj;
        }
    }
    public interface IA
    {

    }
}
