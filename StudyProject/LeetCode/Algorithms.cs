using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LeetCode
{
    public static class Algorithms
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
            for (var i = 0; i < s.Length / 2; i++)
            {
                if (s[i] != s[s.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 最长公共前缀
        /// 字符串数组，长度不定，找出数组中每个字符串的最长公共前缀
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length < 1)
            {
                return "";
            }
            string result = strs[0];
            for (var i = 0; i < strs.Length; i++)
            {
                var j = 0;
                for (; j < result.Length && j < strs[i].Length; j++)
                {
                    if (result[j] != strs[i][j])
                    {
                        break;
                    }
                }
                result = strs[i].Substring(0, j);
                if (result == "")
                {
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// 三数之和 找出数组中三个相加等于零的数，并组成一个数组
        /// 最后把所有数组组成一个数组返回
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> result = new List<IList<int>>();
            Array.Sort(nums);
            var len = nums.Length;
            for (int i = 0; i < len; i++)
            {
                if (nums[i] > 0) break; // 如果当前数字大于0，则三数之和一定大于0，所以结束循环
                if (i > 0 && nums[i] == nums[i - 1]) continue; // 去重
                int L = i + 1;
                int R = len - 1;
                while (L < R)
                {
                    int sum = nums[i] + nums[L] + nums[R];
                    if (sum == 0)
                    {
                        result.Add(new List<int> { nums[i], nums[L], nums[R] });
                        while (L < R && nums[L] == nums[L + 1]) L++; // 去重
                        while (L < R && nums[R] == nums[R - 1]) R--; // 去重
                        L++;
                        R--;
                    }
                    else if (sum < 0) L++;
                    else if (sum > 0) R--;
                }
            }
            return result;
        }
    }
}
