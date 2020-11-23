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
            if(head==null || head.next == null)
            {
                return head;
            }
            ListNode dummyHead = new ListNode(0),lastSorted,curr;
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
                    while(prev.next.val <= curr.val)
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
    }
}
