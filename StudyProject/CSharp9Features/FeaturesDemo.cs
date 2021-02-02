using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp9Features
{
    public class FeaturesDemo
    {
        public static void ForeachDemo()
        {
            var num = 11;
            foreach(var i in num)
            {
                Console.WriteLine(i);
            }
        }
    }
    public static class IntEnumeratorExtensions
    {
        public static IEnumerator<char> GetEnumerator(this int num)
        {
            return num.ToString().GetEnumerator();
        }
    }
}
