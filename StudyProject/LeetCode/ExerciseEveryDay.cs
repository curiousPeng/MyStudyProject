﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode
{
    public static class ExerciseEveryDay
    {
        /// <summary>
        /// 2020-11-19  移动“0”
        /// 给定一个数组 nums，编写一个函数将所有 0 移动到数组的末尾，
        /// 同时保持非零元素的相对顺序。
        /// </summary>
        /// <param name="nums"></param>
        public static void MoveZeroes(int[] nums)
        {
            var i = 0;
            var j = 0;
            for (i = 0; i < nums.Length; i++)
            {
                if (nums[i] != 0)
                {
                    nums[j++] = nums[i];
                }
            }
            while (j < nums.Length)
            {
                nums[j++] = 0;
            }
        }

        /// <summary>
        /// 2020-11-20 单向链表排序
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode InsertionSortList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            ListNode dummyHead = new ListNode(0), lastSorted, curr;
            dummyHead.next = head;
            lastSorted = head;
            curr = head.next;
            while (curr != null)
            {
                if (lastSorted.val <= curr.val)
                {
                    lastSorted = lastSorted.next;
                }
                else
                {
                    ListNode prev = dummyHead;
                    while (prev.next.val <= curr.val)
                    {
                        prev = prev.next;
                    }
                    lastSorted.next = curr.next;
                    curr.next = prev.next;
                    prev.next = curr;
                }
                curr = lastSorted.next;
            }
            return dummyHead.next;
        }
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
        /// <summary>
        /// 2020-11-23 找到数组中，最少需要多个个数字能贯穿全部数组
        /// xstart ≤ x ≤ xend 代表可以贯穿
        /// </summary>
        /// <param name="points"></param>
        public static int FindMinArrowShots(int[][] points)
        {
            if (points.Length < 1)
            {
                return 0;
            }
            Array.Sort(points, (int[] a, int[] b) => a[0] > b[0] ? 1 : -1);

            int result = 1;
            var l = points[points.Length - 1][0];
            for (var i = points.Length - 1; i >= 0; i--)
            {
                if (points[i][1] < l)
                {
                    l = points[i][0];
                    result++;
                }
            }
            return result;
        }

        public static int CountNodes(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            int left = countLevel(root.left);//计算层级
            int right = countLevel(root.right);
            if (left == right)
            {//如果左右层级相等，说明他是满二叉树，左边是必满的，所以计算右边与之相加
                return CountNodes(root.right) + (1 << left);
            }
            else
            {//不相等，说明右数是不满的，但倒数第二层肯定是满的，直接2^right得到右边全部数量，然后左边重新计算。
                return CountNodes(root.left) + (1 << right);
            }
        }
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
        /// <summary>
        /// 2020-11-24 计算二叉树层级
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private static int countLevel(TreeNode root)
        {
            int level = 0;
            while (root != null)
            {
                level++;
                root = root.left;
            }
            return level;
        }
        /// <summary>
        /// 2020-11-25 上升下降字符串
        /// 从 s 中选出 最小 的字符，将它 接在 结果字符串的后面。
        ///从 s 剩余字符中选出 最小 的字符，且该字符比上一个添加的字符大，将它 接在 结果字符串后面。
        ///重复步骤 2 ，直到你没法从 s 中选择字符。
        ///从 s 中选出 最大 的字符，将它 接在 结果字符串的后面。
        ///从 s 剩余字符中选出 最大 的字符，且该字符比上一个添加的字符小，将它 接在 结果字符串后面。
        ///重复步骤 5 ，直到你没法从 s 中选择字符。
        ///重复步骤 1 到 6 ，直到 s 中所有字符都已经被选过
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SortString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            List<char> res = new List<char>();
            int[] num = new int[26];

            for (int i = 0; i < s.Length; i++)
            {
                num[s[i] - 'a']++;
            }

            while (res.Count < s.Length)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (num[i] > 0)
                    {
                        res.Add((char)('a' + i));
                        num[i]--;
                    }
                }

                for (int i = 25; i >= 0; i--)
                {
                    if (num[i] > 0)
                    {
                        res.Add((char)('a' + i));
                        num[i]--;
                    }
                }
            }

            return res.ToString();
        }
        /// <summary>
        /// 2020-11-26 最大间距
        /// 给定一个无序的数组，找出数组在排序之后，相邻元素之间最大的差值。
        ///如果数组元素个数小于 2，则返回 0。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MaximumGap(int[] nums)
        {
            int n = nums.Length;
            if (n < 2)
            {
                return 0;
            }
            long exp = 1;
            int[] buf = new int[n];
            int maxVal = nums.Max();
            while (maxVal >= exp)
            {
                int[] cnt = new int[10];
                for (int i = 0; i < n; i++)
                {
                    int digit = (nums[i] / (int)exp) % 10;
                    cnt[digit]++;
                }
                for (int i = 1; i < 10; i++)
                {
                    cnt[i] += cnt[i - 1];
                }
                for (int i = n - 1; i >= 0; i--)
                {
                    int digit = (nums[i] / (int)exp) % 10;
                    buf[cnt[digit] - 1] = nums[i];
                    cnt[digit]--;
                }
                Array.Copy(buf, 0, nums, 0, n);
                exp *= 10;
            }

            int ret = 0;
            for (int i = 1; i < n; i++)
            {
                ret = Math.Max(ret, nums[i] - nums[i - 1]);
            }
            return ret;
        }

        /// <summary>
        /// 2020-11-27 四数相加
        /// 给定四个包含整数的数组列表 A , B , C , D ,计算有多少个元组 (i, j, k, l) ，使得 A[i] + B[j] + C[k] + D[l] = 0。
        ///为了使问题简单化，所有的 A, B, C, D 具有相同的长度 N，且 0 ≤ N ≤ 500 。所有整数的范围在 -228 到 228 - 1 之间，最终结果不会超过 231 - 1 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <returns></returns>
        public static int fourSumCount(int[] A, int[] B, int[] C, int[] D)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int res = 0;
            for (int i = 0; i < A.Length; i++)
            {
                for (int j = 0; j < B.Length; j++)
                {
                    int sum = A[i] + B[j];
                    if (dic.ContainsKey(sum))
                    {
                        dic[sum] = dic.GetValueOrDefault(sum) + 1;
                    }
                    else
                    {
                        dic.Add(sum, 1);
                    }
                }
            }
            for (int i = 0; i < C.Length; i++)
            {
                for (int j = 0; j < D.Length; j++)
                {
                    int sumCD = -(C[i] + D[j]);
                    if (dic.ContainsKey(sumCD)) res += dic.GetValueOrDefault(sumCD);
                }
            }
            return res;
        }
        /// <summary>
        /// 2020-11-30 给定一个字符串S，检查是否能重新排布其中的字母，使得两相邻的字符不同。
        ///若可行，输出任意可行的结果。若不可行，返回空字符串 12.1才解出答案。
        /// </summary>
        /// <param name="S"></param> 
        /// <returns></returns>
        public static string ReorganizeString(string S)
        {
            var len = S.Length;
            if (len < 2)
            {
                return "";
            }
            Dictionary<char, int> heap = S.GroupBy(x => x)
                    .OrderByDescending(x => x.Count())
                    .ToDictionary(x => x.Key, x => x.Count());
            var max = heap.First();//最多的那个
            if (len % 2 == 0)
            {//偶数
                if (max.Value > (len / 2))
                {
                    return "";
                }
            }
            else
            {//奇数
                if (max.Value > ((len + 1) / 2))
                {
                    return "";
                }
            }
            string result = string.Empty;
            while (result.Length < len)
            {

                int c = 0;
                for (int j = 0; j < heap.Count; j++)
                {
                    var item = heap.ElementAt(j);
                    if (item.Value > 0)
                    {
                        c++;
                        if (result.Length > 0 && result[result.Length - 1] == item.Key)
                        {
                            continue;
                        }
                        else
                        {
                            result = string.Format("{0}{1}", result, item.Key);
                            heap[item.Key] = heap[item.Key] - 1;

                            if (c >= 2)
                            {
                                break;
                            }
                        }
                    }
                }
                var over = len - result.Length;
                if (over > 1 && over == heap.Last().Value)
                {//说明剩最后重复的几个了
                    break;
                }
            }
            var last = heap.Last();
            for (var i = 0; i < result.Length; i++)
            {
                if (heap[last.Key] < 1)
                {
                    break;
                }
                if (result[i] != last.Key)
                {
                    result = string.Format("{0}{1}{2}", result.Substring(0, i + 1), last.Key, result.Substring(i + 1, result.Length - i - 1));
                    heap[last.Key] = heap[last.Key] - 1;
                }
            }
            return result;
        }
        /// <summary>
        /// 2020-12-01给定一个按照升序排列的整数数组 nums，和一个目标值 target。找出给定目标值在数组中的开始位置和结束位置。
        ///如果数组中不存在目标值 target，返回[-1, -1]
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] SearchRange(int[] nums, int target)
        {
            int[] result = new int[2] { -1, -1 };
            if (nums.Length < 1)
            {
                return result;
            }
            if (nums.Length == 1)
            {
                if (nums[0] != target)
                {
                    return result;
                }
                result[0] = 0;
                result[1] = 0;
                return result;
            }
            var left = 0;
            var right = nums.Length - 1;
            while (left <= right)
            {

                if (nums[left] == target)
                {
                    if (result[0] == -1)
                    {
                        result[0] = left;
                    }
                    else
                    {
                        if (result[0] > left)
                        {
                            result[0] = left;
                        }
                        if (result[1] < left)
                        {
                            result[1] = left;
                        }
                    }

                }
                if (nums[right] == target)
                {
                    if (result[1] == -1)
                    {
                        result[1] = right;
                    }
                    else
                    {
                        if (result[0] == -1)
                        {
                            result[0] = right;
                        }
                        if (result[0] > right)
                        {
                            result[0] = right;
                        }
                        if (result[1] < right)
                        {
                            result[1] = right;
                        }
                    }
                }
                left++;
                right--;
            }
            if (result[0] != -1 && result[1] == -1)
            {
                result[1] = result[0];
            }
            if (result[1] != -1 && result[0] == -1)
            {
                result[0] = result[1];
            }
            return result;
        }
        /// <summary>
        /// 2020-12-02给定长度分别为 m 和 n 的两个数组，其元素由 0-9 构成，表示两个自然数各位上的数字。
        /// 现在从这两个数组中选出 k (k <= m + n) 个数字拼接成一个新的数，
        /// 要求从同一个数组中取出的数字保持其在原数组中的相对顺序。
        ///求满足该条件的最大数。结果返回一个表示该最大数的长度为 k 的数组
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static int[] MaxNumber(int[] nums1, int[] nums2, int k)
        {
            int m = nums1.Length, n = nums2.Length;
            int[] maxSubsequence = new int[k];
            int start = Math.Max(0, k - n), end = Math.Min(k, m);
            for (int i = start; i <= end; i++)
            {
                int[] subsequence1 = PickMax(nums1, i);
                int[] subsequence2 = PickMax(nums2, k - i);
                int[] curMaxSubsequence = merge(subsequence1, subsequence2);
                if (compare(curMaxSubsequence, 0, maxSubsequence, 0) > 0)
                {
                    Array.Copy(curMaxSubsequence, 0, maxSubsequence, 0, k);
                }
            }
            return maxSubsequence;
        }
        private static int[] PickMax(int[] nums, int k)
        {
            int length = nums.Length;
            int[] stack = new int[k];
            int top = -1;
            int remain = length - k;
            for (int i = 0; i < length; i++)
            {
                int num = nums[i];
                while (top >= 0 && stack[top] < num && remain > 0)
                {
                    top--;
                    remain--;
                }
                if (top < k - 1)
                {
                    stack[++top] = num;
                }
                else
                {
                    remain--;
                }
            }
            return stack;
        }
        private static int[] merge(int[] subsequence1, int[] subsequence2)
        {
            int x = subsequence1.Length, y = subsequence2.Length;
            if (x == 0)
            {
                return subsequence2;
            }
            if (y == 0)
            {
                return subsequence1;
            }
            int mergeLength = x + y;
            int[] merged = new int[mergeLength];
            int index1 = 0, index2 = 0;
            for (int i = 0; i < mergeLength; i++)
            {
                if (compare(subsequence1, index1, subsequence2, index2) > 0)
                {
                    merged[i] = subsequence1[index1++];
                }
                else
                {
                    merged[i] = subsequence2[index2++];
                }
            }
            return merged;
        }
        private static int compare(int[] subsequence1, int index1, int[] subsequence2, int index2)
        {
            int x = subsequence1.Length, y = subsequence2.Length;
            while (index1 < x && index2 < y)
            {
                int difference = subsequence1[index1] - subsequence2[index2];
                if (difference != 0)
                {
                    return difference;
                }
                index1++;
                index2++;
            }
            return (x - index1) - (y - index2);
        }
        /// <summary>
        /// 2020-12-03统计所有小于非负整数 n 的质数的数量。
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CountPrimes(int n)
        {
            if (n < 3)
            {
                return 0;
            }
            int ans = 1;
            for (int i = 3; i < n; ++i)
            {
                if ((i & 1) == 0)
                    continue;
                ans += IsPrime(i) ? 1 : 0;
            }
            return ans;
        }

        private static bool IsPrime(int x)
        {
            for (int i = 3; i * i <= x; i += 2)
            {
                if (x % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 给你一个按升序排序的整数数组 num（可能包含重复数字）
        /// 请你将它们分割成一个或多个子序列，
        /// 其中每个子序列都由连续整数组成且长度至少为 3 。
        ///如果可以完成上述分割，则返回 true ；否则，返回 false 。
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool IsPossible(int[] nums)
        {
            if (nums.Length < 3)
            {
                return false;
            }
            //新建两个字典
            Dictionary<int, int> dic1 = new Dictionary<int, int>(); //存储原数组中数字i出现的次数
            Dictionary<int, int> dic2 = new Dictionary<int, int>();//存储以数字i结尾的且符合题意的连续子序列个数
            //以nums =[1, 2, 3, 3, 4, 4, 5]
            //初始化：dic1[1] = 1、dic1[2] = 1、dic1[3] = 2、dic1[4] = 2、dic1[5] = 1，dic2[i]都为0
            foreach (var item in nums)
            {
                if (dic1.ContainsKey(item))
                {
                    dic1[item]++;
                }
                else
                {
                    dic1.Add(item, 1);
                }
            }
            foreach (var item in nums)
            {
                //检查数字 1, dic1[1] > 0,并且 dic1[2]> 0,dic1[3] > 0，因此找到了一个长度为3的连续子序列 dic1[1]、dic1[2]、dic1[3] 各自减一，并 dic2[3] 加 1
                int count = dic1[item];
                if (count > 0)
                {
                    //判断item上一个结尾
                    int prevCount = dic2.GetValueOrDefault(item - 1, 0);
                    //如果大于0就就把item接到dic2上,dic2[item-1]-1;dic2[item]+1
                    if (prevCount > 0)
                    {
                        dic1[item] = count - 1;
                        if (dic2.ContainsKey(item - 1))
                        {
                            dic2[item - 1] = prevCount - 1;
                        }
                        else
                        {
                            dic2.Add(item - 1, prevCount - 1);
                        }
                        if (dic2.ContainsKey(item))
                        {
                            dic2[item] = dic2.GetValueOrDefault(item, 0) + 1;
                        }
                        else
                        {
                            dic2.Add(item, dic2.GetValueOrDefault(item, 0) + 1);
                        }

                    }
                    else
                    {
                        //但是 dic2[2]=0，因此不能接在前面，只能往后看(如果后面组不成，那就返回 false了)
                        int count1 = dic1.GetValueOrDefault(item + 1, 0);
                        int count2 = dic1.GetValueOrDefault(item + 2, 0);
                        if (count1 > 0 && count2 > 0)
                        {
                            //实际发现 dic1[4]>0,dic1[5]>0，因此找到了一个长度为3的连续子序列 dic1[3]、dic1[4]、dic1[5] 各自减一，并 dic2[5] 加 1
                            dic1[item] = count - 1;
                            dic1[item + 1] = count1 - 1;
                            dic1[item + 2] = count2 - 1;
                            if (dic2.ContainsKey(item + 2))
                            {
                                dic2[item + 2] = dic2[item + 2] + 1;
                            }
                            else
                            {
                                dic2.Add(item + 2, 1);
                            }

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="senate"></param>
        /// <returns></returns>
        public static string PredictPartyVictory(string senate)
        {
            Queue<int> R = new Queue<int>();
            Queue<int> D = new Queue<int>();
            for(var i = 0; i < senate.Length; i++)
            {
                if (senate[i] == 'R')
                {
                    R.Enqueue(i);
                }
                else
                {
                    D.Enqueue(i);
                }
            }

            while (R.Count > 0 && D.Count > 0)
            {
                var a = R.Dequeue();
                var b = D.Dequeue();
                if (a < b)
                {
                    R.Enqueue(senate.Length + a);
                }
                else
                {
                    D.Enqueue(senate.Length + b);
                }
            }
            if (R.Count > 0)
            {
                return "Radiant";
            }else
            {
                return "Dire";
            }
        }
    }
}
