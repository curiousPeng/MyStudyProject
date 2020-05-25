using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.CSharp8
{
    class StaticLocalFunctions
    {

        ///Static local functions 静态本地函数

        ///向本地函数添加 static 修饰符，以确保本地函数不会从封闭范围引用任何变量
        int M()
        {
            int y = 5;
            int x = 7;
            return Add(x, y);

            static int Add(int left, int right) => left + right;
        }
    }
}
