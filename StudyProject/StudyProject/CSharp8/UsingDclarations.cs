using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.CSharp8
{
    class UsingDclarations
    {
        /// <summary>
        /// using 关键字使用优化，下面代码等价于using(){}
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        static int WriteLinesToFile(IEnumerable<string> lines, string path)
        {
            using var file = new System.IO.StreamWriter(path);
            int skippedLines = 0;
            foreach (var line in lines)
            {
                if (!line.Contains("Second"))
                {
                    file.WriteLine(line);
                }
                else
                {
                    skippedLines++;
                }
            }
            return skippedLines;
        }
    }
}
