using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.ThreadPoolDemo
{
    public class Demo
    {
        private static IOQueue[] work_pool = new IOQueue[4];//开四个线程

        public IOQueue worker;
        static Demo()
        {
            for (int i = 0; i < 4; i++)
            {
                work_pool[i] = new IOQueue();//创建四个线程
            }
        }
        public void DemoMain()
        {
            for(int i = 0;i < 20; i++)
            {
                var a = new DemoWork();
                var b = i + "aaa";
                a._work = work_pool[i % 4];//开了四个线程来处理这些任务，把任务平均分配到四个线程上
                a._work.QueueTask(t =>
                {
                    var obj = t as string;
                    PrintString(obj);
                }, b);
            }
        }

        public void PrintString(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                Console.WriteLine("aaaa");
            }
            else
            {
                Console.WriteLine(str);
            }
        }
    }
    public class DemoWork
    {
        public IOQueue _work;
    }
}
