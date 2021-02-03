using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Features
{
    class RecordDemo
    {
        public static void demo()
        {
            var p1 = new RecordPerson()
            {
                Name = "Tom",
                Age = 12,
            };
            Console.WriteLine(p1);

            var p2 = p1 with { Age = 10 };
            Console.WriteLine(p2);

            var p3 = new RecordPerson() { Name = "Tom", Age = 12 };
            Console.WriteLine(p3);
            Console.WriteLine($"p1 Equals p3 =:{p1 == p3}");

            RecordPersonOther p4 = new("Tom", 12);
            Console.WriteLine(p4);
        }
    }

    record RecordPerson
    {
        public string Name { get; init; }
        public int Age { get; init; }
    }
    record RecordPersonOther(string Name,int Age);
}
