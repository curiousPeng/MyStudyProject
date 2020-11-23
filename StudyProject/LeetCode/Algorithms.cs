using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LeetCode
{
    public class Algorithms
    {
        /// <summary>
        /// 整数是否是回文
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome(int x)
        {
            if (x < 0)
            {
                return false;
            }
            if (x < 10)
            {
                return true;
            }
            var s = x.ToString();
            if (s[0] != s[s.Length - 1])
            {
                return false;
            }
            for(var i = 0; i < s.Length/2; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
