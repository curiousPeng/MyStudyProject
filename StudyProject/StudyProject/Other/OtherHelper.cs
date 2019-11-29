using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StudyProject.Other
{
    public static class OtherHelper
    {
        /**
         * 将带下滑线的英文字段转换为驼峰式命名
         */
        public static string ConvertToCamelCase(string str)
        {
            str = str.ToLower();
            var strArray = str.Split("_");
            var sb = new StringBuilder();
            foreach (var word in strArray)
            {
                sb.Append(string.Format("{0}{1}", word.First().ToString().ToUpper(), word.Substring(1)));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 冒泡排序泛型+委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortArray"></param>
        /// <param name="fun"></param>
        public static void Sort<T>(IList<T> sortArray, Func<T, T, bool> fun)
        {
            bool swapped = true;
            do
            {
                swapped = false;
                for (var i = 0; i < sortArray.Count - 1; i++)
                {
                    if (fun(sortArray[i + 1], sortArray[i]))
                    {
                        T temp = sortArray[i];
                        sortArray[i] = sortArray[i + 1];
                        sortArray[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
        }
        public static bool CompareInt(int a, int b)
        {
            if (a > b) return false;
            return true;
        }

        public static byte[] StreamToBytes(MemoryStream memStream)
        {
            int count;
            byte[] byteArray;
            Console.WriteLine(
                "Capacity = {0}, Length = {1}, Position = {2}\n",
                memStream.Capacity.ToString(),
                memStream.Length.ToString(),
                memStream.Position.ToString());

            // Set the position to the beginning of the stream.
            memStream.Seek(0, SeekOrigin.Begin);

            // Read the first 20 bytes from the stream.
            byteArray = new byte[memStream.Length];
            count = memStream.Read(byteArray, 0, byteArray.Length);

            // Read the remaining bytes, byte by byte.
            while (count < memStream.Length)
            {
                byteArray[count++] =
                    Convert.ToByte(memStream.ReadByte());
            }
            // stream.Seek(0, SeekOrigin.Begin);
            // stream.Dispose();
            return byteArray;

        }
    }
}
