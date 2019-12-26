using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.DesignPattern.Adapter
{
    /// <summary>
    /// 适配器模式
    /// </summary>
    public class AdapterDemo: IScoreOperation
    {
        private QuickSortHelper sortTarget;
        private BinarySearchHelper searchTarget;

        public AdapterDemo()
        {
            sortTarget = new QuickSortHelper();
            searchTarget = new BinarySearchHelper();
        }

        public int Search(int[] array, int key)
        {
            return searchTarget.BinarySearch(array, key);
        }

        public int[] Sort(int[] array)
        {
            return sortTarget.QuickSort(array);
        }
    }

    /// <summary>
    /// 目标接口：抽象成绩操作类    
    /// </summary>
    public interface IScoreOperation
    {        // 成绩排序
        int[] Sort(int[] array);        // 成绩查找
        int Search(int[] array, int key);
    }

    /// <summary>
    /// 适配者A：快速排序类    
    /// </summary>
    public class QuickSortHelper
    {
        public int[] QuickSort(int[] array)
        {
            Sort(array, 0, array.Length - 1); return array;
        }
        public void Sort(int[] array, int p, int r)
        {
            int q = 0; if (p < r)
            {
                q = Partition(array, p, r);
                Sort(array, p, q - 1);
                Sort(array, q + 1, r);
            }
        }
        public int Partition(int[] array, int p, int r)
        {
            int x = array[r]; int j = p - 1; for (int i = p; i <= r - 1; i++)
            {
                if (array[i] <= x)
                {
                    j++;
                    Swap(array, j, i);
                }
            }

            Swap(array, j + 1, r); return j + 1;
        }
        public void Swap(int[] array, int i, int j)
        {
            int t = array[i];
            array[i] = array[j];
            array[j] = t;
        }
    }

    public class BinarySearchHelper
    {
        public int BinarySearch(int[] array, int key)
        {
            int low = 0;
            int high = array.Length - 1;

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int midVal = array[mid];

                if (midVal < key)
                {
                    low = mid + 1;
                }
                else if (midVal > key)
                {
                    high = mid - 1;
                }
                else
                {
                    return 1;   // 找到元素返回1
                }
            }

            return -1;  // 未找到元素返回-1
        }
    }
}
