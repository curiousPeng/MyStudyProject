using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
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
            Parallel.Invoke(Foo, Bar);
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
    #region  争用条件和死锁

    //争用条件例子
    public class StateObject
    {
        private int _state = 5;
        private object _lock = new object();
        public void ChangeState(int loop)
        {
            lock (_lock)//有此lock语句不会出现争用条件
            {
                if (_state == 5)
                {
                    _state++;
                    Trace.Assert(_state == 6, $"Race condition occurred after {loop} loops");
                }
                _state = 5;
            }
        }
    }
    public class SampleTaskRace
    {
        public void RaceCondition(object o)
        {
            Trace.Assert(o is StateObject, "o must be of typeStateObject");
            StateObject state = o as StateObject;

            int i = 0;
            while (true)
            {
                lock (state)//lock语句不会让争用条件出现，如要实验争用条件则需要注销改语句。
                {
                    state.ChangeState(i++);
                }

            }
        }
        public void RaceConditions()
        {
            var state = new StateObject();
            for (int i = 0; i < 2; i++)
            {
                Task.Run(() => new SampleTaskRace().RaceCondition(state));
            }
        }
    }

    //死锁例子
    public class SampleTaskLock
    {
        private StateObject _s1;
        private StateObject _s2;
        public SampleTaskLock(StateObject s1, StateObject s2)
        {
            _s1 = s1;
            _s2 = s2;
        }

        public void Deadlock1()
        {
            int i = 0;
            while (true)
            {
                lock (_s1)
                {
                    lock (_s2)
                    {
                        _s1.ChangeState(i);
                        _s2.ChangeState(i++);
                        Console.WriteLine($"still runing,{i}");
                    }
                }
            }
        }
        public void Deadlock2()
        {
            int i = 0;
            while (true)
            {
                lock (_s1)
                {
                    lock (_s2)
                    {
                        _s1.ChangeState(i);
                        _s2.ChangeState(i++);
                        Console.WriteLine($"still runing,{i}");
                    }
                }
            }
        }
    }
    #endregion

    #region lock语句和线程安全
    public class SharedState
    {
        public int State { get; set; }
    }
    public class Job
    {
        private SharedState _sharedState;
        private object _lock = new object();
        public Job(SharedState sharedState)
        {
            _sharedState = sharedState;
        }
        /// <summary>
        /// 创建个对象递增2000次，然后下面开多个线程来一起跑，
        /// </summary>
        public void DoTheJob()
        {
            for (int i = 0; i < 2000; i++)
            {
                _sharedState.State += 1;
            }
        }
        /// <summary>
        /// 开启20个任务跑同一个对象，本来应该会得到40W这个数字，由于没有同步进行，几乎不会得到正确的输出值
        /// state1加了lock,会得到一个正确的值
        /// </summary>
        public static void RunTask()
        {
            int numTasks = 20;
            var state = new SharedState();
            var state1 = new SharedState();
            var tasks = new Task[numTasks];
            var tasks1 = new Task[numTasks];
            for (var i = 0; i < numTasks; i++)
            {
                tasks[i] = Task.Run(() => { new Job(state).DoTheJob(); });
                tasks1[i] = Task.Run(() => new Job(state).DoThatJob());
            }
            Task.WaitAll(tasks);
            Task.WaitAll(tasks1);
            Console.WriteLine($"state summarized {state.State}");
            Console.WriteLine($"state1 summarized {state1.State}");
        }
        /// <summary>
        /// 加上lock语句，让线程每次只能有一个进入，使之变成同步，这样就会得到正确的值
        /// </summary>
        public void DoThatJob()
        {

            for (int i = 0; i < 2000; i++)
            {
                lock (_lock)
                {
                    _sharedState.State += 1;
                }
            }
        }
    }

    //Interlocked 此类同比其他同步技术，会快得多，只适合用于简单的同步问题
    public class InterlockedDemo
    {
        private int _state = 0;

        public int StateSyn
        {
            get { return Interlocked.Increment(ref _state); }
        }
    }

    //Monitor lock语句会被c#解析器翻译为Monitor类。
    public class MonitorDemo
    {
        private object _lock = new object();
        public void demo1()
        {
            lock (_lock)
            {
                ///
            }
            //上面方法会被翻译成
            Monitor.Enter(_lock);
            try
            {
                ///
            }
            finally
            {
                Monitor.Exit(_lock);
            }
        }

    }
    #endregion
}
