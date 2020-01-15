using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyProject.ThreadPoolDemo
{
    internal static class CancellTaskSource
    {
        public static void CancellTaskDemo()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> t = Task.Run(() => Sum(cts.Token, 100));
            cts.Cancel();
            try
            {
                Console.WriteLine("The sum is:" + t.Result);
            }
            catch (AggregateException ex)
            {
                ex.Handle(e => e is OperationCanceledException);
                Console.WriteLine(" Sum was Canceled");
            }
        }

        public static void StartANewTask()
        {
            Task<int> t = Task.Run(() => Sum(10000));
            //Task cwt = t.ContinueWith(task => Console.WriteLine("The sum is:" + t.Result));
            t.ContinueWith(task => Console.WriteLine("The sum is:" + task.Result), TaskContinuationOptions.OnlyOnRanToCompletion);
            t.ContinueWith(task => Console.WriteLine("Sum throw:" + task.Exception), TaskContinuationOptions.OnlyOnFaulted);
            t.ContinueWith(task => Console.WriteLine("Sum was canceled"), TaskContinuationOptions.OnlyOnCanceled);
            try
            {
                t.Wait();  // For the testing only,synchronous
            }
            catch (AggregateException)
            {
            }
        }

        private static int Sum(CancellationToken ct, int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                ct.ThrowIfCancellationRequested();
                checked { sum += n; }
            }
            return sum;
        }

        private static int Sum(int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                checked { sum += n; }
            }
            return sum;
        }
    }
}
