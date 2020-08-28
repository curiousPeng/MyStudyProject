using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StudyProject.Algorithms
{
    public static class AlgorithmsSolution
    {
        /// <summary>
        /// 两数之和
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            var isOk = false;
            var result = new int[2];
            for (var i = 0; i < nums.Length; i++)
            {
                for (var j = 1; j < nums.Length; j++)
                {
                    if (i == j)
                    {
                        j++;
                        if (j == nums.Length)
                        {
                            return null;
                        }
                    }
                    var tmp = nums[i] + nums[j];
                    if (tmp == target)
                    {
                        isOk = true;
                        result[0] = i;
                        result[1] = j;
                        Console.Write($"[{i},{j}]");
                        return result;
                    }
                }
            }
            if (!isOk)
            {
                Console.Write("未找到合适的答案");
                return null;
            }
            return result;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }

        /// <summary>
        /// 两数求和
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            int intTemp = 0;
            var flag = true;
            ListNode head = null;
            ListNode next = null;
            while (flag)
            {
                if (l1 != null)
                {
                    if (l2 != null)
                    {
                        var res = l1.val + l2.val + intTemp;
                        intTemp = res >= 10 ? 1 : 0;
                        if (head == null)
                        {
                            head = new ListNode(res % 10);
                            next = head;
                        }
                        else
                        {
                            var t = new ListNode(res % 10);
                            next.next = t;
                            next = next.next;
                        }
                        l2 = l2.next;
                    }
                    else
                    {
                        var res = l1.val + intTemp;
                        intTemp = res >= 10 ? 1 : 0;
                        var t = new ListNode(res % 10);
                        next.next = t;
                        next = next.next;
                    }
                    l1 = l1.next;
                }
                else
                {
                    if (l2 != null)
                    {
                        var res = l2.val + intTemp;
                        intTemp = res >= 10 ? 1 : 0;
                        var t = new ListNode(res % 10);
                        next.next = t;
                        next = next.next;
                        l2 = l2.next;
                    }
                    else
                    {
                        if (intTemp == 1)
                        {
                            var t = new ListNode(intTemp);
                            next.next = t;
                            next = next.next;
                        }
                        flag = false;
                    }
                }
            }
            return head;
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            while (l1 != null)
            {
                a.Add(l1.val);
                l1 = l1.next;
            }
            while (l2 != null)
            {
                b.Add(l2.val);
                l2 = l2.next;
            }

            List<int> c = new List<int>();
            List<int> Short = a.Count < b.Count ? a : b;
            List<int> Long = a.Count < b.Count ? b : a;

            int Carry = 0;
            for (int i = 0; i < Short.Count; i++)
            {
                c.Add((Long[i] + Short[i] + Carry) % 10);
                Carry = (Long[i] + Short[i] + Carry) >= 10 ? 1 : 0;
            }
            for (int i = Short.Count; i < Long.Count; i++)
            {
                c.Add((Long[i] + Carry) % 10);
                Carry = (Long[i] + Carry) >= 10 ? 1 : 0;
            }
            if (Carry == 1) { c.Add(1); }
            ListNode result = new ListNode(c[0]);
            var temp = result;
            for (var i = 0; i < c.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                var t = new ListNode(c[i]);
                temp.next = t;
                temp = temp.next;
            }
            return result;
        }

        /// <summary>
        /// 找出最长无重复子串的个数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            List<char> ls = new List<char>();
            int n = s.Length;
            int intMaxLength = 0;
            for (int i = 0; i < n; i++)
            {
                if (ls.Contains(s[i]))
                {
                    ls.RemoveRange(0, ls.IndexOf(s[i]) + 1);
                }
                ls.Add(s[i]);
                intMaxLength = ls.Count > intMaxLength ? ls.Count : intMaxLength;
            }
            return intMaxLength;
        }
        /// <summary>
        /// 寻找两个有序数组的中位数
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int[] num = new int[nums1.Length + nums2.Length];
            nums1.CopyTo(num, 0);
            nums2.CopyTo(num, nums1.Length);
            Array.Sort(num);
            int a = 0;
            int b = 0;
            if (num.Length > 1)
            {
                if (num.Length % 2 == 0)
                {
                    a = num[num.Length / 2];
                    b = num[(num.Length / 2) - 1];
                    return (a + b) / 2;
                }
                else
                {
                    a = num[num.Length / 2];
                }
            }
            else
            {
                a = num[0];

            }

            return a;
        }

        /// <summary>
        /// 整数反转
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Reverse(int x)
        {
            int tmp = 0;
            bool isLtZero = false;
            if (x < 0)
            {
                isLtZero = true;
                if (x == -2147483648)
                {
                    return 0;
                }
                x = Math.Abs(x);
            }
            while (x > 0)
            {
                int a = x % 10;
                x = x / 10;

                try { checked { tmp = tmp * 10 + a; } }
                catch
                {

                    return 0;
                }
            }
            if (isLtZero)
            {
                tmp *= -1;
            }
            return tmp;
        }

        /// <summary>
        /// 找出开头的数，其余返回零
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int MyAtoi(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            StringBuilder a = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ' && a.Length > 0)
                {
                    break;
                }
                if (str[i] == ' ')
                {
                    continue;
                }
                var b = (short)str[i];
                if (b > 64)
                {
                    break;
                }
                if (a.Length == 0 && b < 48 && b != 45 && b != 43)
                {
                    return 0;
                }
                if (a.Length > 0 && b < 48)
                {
                    break;
                }
                a.Append(str[i].ToString());
            }
            if (a.Length == 0)
            {
                return 0;
            }
            var c = a.ToString();
            if (c == "-" || c == "+")
            {
                return 0;
            }
            int result;
            try
            {
                result = int.Parse(c);
            }
            catch (System.OverflowException)
            {
                if (c.StartsWith("-"))
                {
                    result = int.MinValue;
                }
                else
                {
                    result = int.MaxValue;
                }
            }

            return result;
        }

        /// <summary>
        /// 根据一组数组，成员值表示y轴高度，每个值间距1，计算最大区域，
        /// 此为暴力解法，数组过大会用时过长，最优解法是双指针法
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int MaxArea(int[] height)
        {
            int area = 0;
            for (var i = 0; i < height.Length; i++)
            {
                for (var j = i + 1; j < height.Length; j++)
                {
                    area = Math.Max(area, Math.Min(height[i], height[j]) * (j - i));
                }
            }
            return area;
        }

        /// <summary>
        /// 双指针法
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int MaxAreaOfPointers(int[] height) {
            int i = 0;
            int j = height.Length - 1;
            int area = 0;
            while (i < j) {
                area = Math.Max(area, Math.Min(height[i], height[j]) * (j - i));
                if (height[i] < height[j]) {
                    i++;
                }
                else
                {
                    j--;
                }
            }
            return area;
        }


    }
}

