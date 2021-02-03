using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Features
{
    public class EnhancedMatchingPattern
    {
        public static void Sample()
        {
            var p = new Person() { Age = 10, Name = "张三" };
            if (p is not null and { Age: > 0 })
            {
                Console.WriteLine(p.Age);
            }
            if (p.Name is not null or { Length: > 0 })
            {
                if (p.Name[0] is (> 'a' and < 'z'))
                {
                    Console.WriteLine(p.Name);
                }
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
