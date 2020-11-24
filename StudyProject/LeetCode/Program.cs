using System;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] str = { -1, 0, 1, 2, -1, -4 };

           var a= Algorithms.ThreeSum(str);

            Console.WriteLine("program execute finish！");
            Console.Read();
        }
    }
}
