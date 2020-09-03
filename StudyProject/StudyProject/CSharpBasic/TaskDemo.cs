using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.CSharpBasic
{
    public class TaskDemo
    {
        //使用Parallel.For()循环
        //并行运行迭代，迭代顺序没有保证。
        public static void Log(string prefix)
        {
            Console.WriteLine($"{prefix},task:{Task.CurrentId},thread:{System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }
        /// <summary>
        /// 此方法可以看出该迭代顺序不能保证。
        /// </summary>
        public static void ParallerFor()
        {
            ParallelLoopResult result = Parallel.For(0, 10, i =>
            {
                Log($"S：{i}");
                Task.Delay(10).Wait();//阻塞线程给到更多的机会创建线程和任务，不然这儿所能看到的线程和任务会更少
                Log($"E：{i}");
            });
            Console.WriteLine($"Is completed:{result.IsCompleted}");
        }
        public static void ParallerForWithAsync()
        {
            ParallelLoopResult result =
                Parallel.For(0, 10, async i =>
                {
                    Log($"S：{i}");
                    await Task.Delay(10);//开启异步后，S 会全部先打印结束，而且S和E不是同一个线程执行的
                    Log($"E：{i}");
                });//Parallel.For()会直接执行完毕，有可能甚至看不到打印e。
            Console.WriteLine($"Is completed:{result.IsCompleted}");
        }

        //提前停止Parallel.For()

        /// <summary>
        /// 
        /// </summary>
        public static void StopParallelForEarly()
        {
            ParallelLoopResult result =
                Parallel.For(10, 40, (int i, ParallelLoopState pls) =>
                {
                    Log($"S:{i}");
                    if (i > 20)
                    {
                        pls.Break();
                        Log($"break now... {i}");
                    }
                    Task.Delay(10).Wait();
                    Log($"E: {i}");
                });
            Console.WriteLine($"Is Completed:{result.IsCompleted}");
            Console.WriteLine($"lowest brak iteration:{result.LowestBreakIteration}");
        }

        ///并行任务
        
        public static void ParallelInvoke()
        {
            Parallel.Invoke(Foo,Bar);
        }
        public static void Foo()
        {
            Console.WriteLine("foo");
        }
        public static void Bar()
        {
            Console.WriteLine("bar");
        }

        //连续的任务

        private static void DoOnFirst()
        {
            Console.WriteLine($"doing some task {Task.CurrentId}");
            Task.Delay(3000).Wait();
        }

        private static void DoOnSecond(Task t)
        {
            Console.WriteLine($"task {t.Id} finished");
            Console.WriteLine($"this task id {Task.CurrentId}");
            Task.Delay(3000).Wait();
        }

        public static void ContinuationTasks()
        {
            Task t1 = new Task(DoOnFirst);
            Task t2 = t1.ContinueWith(DoOnSecond);
            t1.Start();
        }
    }
}
