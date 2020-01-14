using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StudyProject.ThreadPoolDemo
{
    /// <summary>
    /// 简易实用的线程池
    /// </summary>
    public sealed class IOQueue
    {
        private static readonly WaitCallback _doWorkCallback = s => ((IOQueue)s).DoWork();

        private readonly object _workSync = new object();
        private readonly ConcurrentQueue<Work> _workItems = new ConcurrentQueue<Work>();
        private bool _doingWork;

        public void QueueTask(Action<object> action, object state)
        {
            var work = new Work
            {
                Callback = action,
                State = state
            };

            _workItems.Enqueue(work);

            lock (_workSync)
            {
                if (!_doingWork)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(_doWorkCallback, this);
                    _doingWork = true;
                }
            }
        }

        private void DoWork()
        {
            while (true)
            {
                while (_workItems.TryDequeue(out Work item))
                {
                    item.Callback(item.State);
                }

                lock (_workSync)
                {
                    if (_workItems.IsEmpty)
                    {
                        _doingWork = false;
                        return;
                    }
                }
            }
        }
        public struct Work
        {
            public Action<object> Callback;
            public object State;
        }
    }
}
