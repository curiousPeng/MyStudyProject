using System;
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
        /// 四数相加
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
    }
}
