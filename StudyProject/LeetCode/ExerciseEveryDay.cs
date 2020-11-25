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
        /// 计算二叉树层级
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
        /// 上升下降字符串
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
    }
}
