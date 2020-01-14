using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;

namespace StudyProject.ThreadPool.Demo
{
    internal static class CancellTaskDemo
    {
        public static void Run()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Token.Register(() => { Console.WriteLine("Cancell Regist 1"); });
            cts.Token.Register(() => { Console.WriteLine("Cancell Regist 2"); });
            System.Threading.ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 100));
            Console.WriteLine("Print Enter stop the count");
            Console.ReadLine();
            cts.Cancel();
        }

        public static void RunDemo()
        {
            CancellationTokenSource cts1 = new CancellationTokenSource();
            cts1.Token.Register(() => Console.WriteLine("cts1  canceled"));

            CancellationTokenSource cts2 = new CancellationTokenSource();
            cts2.Token.Register(() => Console.WriteLine("cts2 canceled"));

            CancellationTokenSource cts3 = CancellationTokenSource.CreateLinkedTokenSource(cts1.Token,cts2.Token); ;
            cts3.Token.Register(() => Console.WriteLine("linkedCts Canceled"));

            cts1.Cancel();
            Console.WriteLine("cts1 canceled={0},cts2 canceled={1},cts3 = {2}",
                cts1.IsCancellationRequested,
                cts2.IsCancellationRequested,
                cts3.IsCancellationRequested);

        }

        private static void Count(CancellationToken token, int countTo)
        {
            for (int count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break;
                }
                Console.WriteLine(count);
                Thread.Sleep(2000);
            }
            Console.WriteLine("Count is done");
        }
    }
}
