using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading
{

    class PriorityQueue<TArgument, TResult>
    {
        private int _nextId = 0;

        private readonly SortedDictionary<long, QueueItem<TArgument, TResult>> _queue = new SortedDictionary<long, QueueItem<TArgument, TResult>>();

        private List<QueueItem<TArgument, TResult>> _processingItems = new List<QueueItem<TArgument, TResult>>();

        private readonly Func<TArgument, Task<TResult>> ItemAction;

        private readonly int MaxThreadsCount;

        private readonly object Synchronizer = new object();

        public PriorityQueue(Func<TArgument, Task<TResult>> processingAction, int maxThreadsCount)
        {
            ItemAction = processingAction;
            MaxThreadsCount = maxThreadsCount;
        }

        public QueueItem<TArgument, TResult> Enqueue(TArgument argument, int priority)
        {
            QueueItem<TArgument, TResult> item = null;
            lock (Synchronizer)
            {
                _nextId++;
                var id = _nextId;
                item = new QueueItem<TArgument, TResult>(priority, argument);
                item.AssociateWithQueue(this, id);
                _queue.Add(item.FullId, item);
            }

            TryProcessItemsInQueue();
            return item;
        }

        public void Enqueue(QueueItem<TArgument, TResult> item)
        {
            lock (Synchronizer)
            {
                _nextId++;
                var id = _nextId;
                item.AssociateWithQueue(this, id);
                _queue[item.FullId] = item;
            }

            TryProcessItemsInQueue();
        }

        public void Cancel(QueueItem<TArgument, TResult> item)
        {
            lock (Synchronizer)
            {
                item.Cancelled = true;
                var isRemoved = _queue.Remove(item.FullId);
                _processingItems.Remove(item);
                item.NotifyAboutCompletion(default(TResult), true, null);
            }

            TryProcessItemsInQueue();
        }

        internal void ChangePriority(QueueItem<TArgument, TResult> item, int newPriority)
        {
            lock (Synchronizer)
            {
                if (item.Status == ExecutionStatus.Enqueued)
                {
                    _queue.Remove(item.FullId);
                }

                item.UpdatePriority(newPriority);
                if (item.Status == ExecutionStatus.Enqueued)
                {
                    _queue[item.FullId] = item;
                }
            }
        }

        private void TryProcessItemsInQueue()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    var item = DequeueItem();
                    if (item == null)
                    {
                        return;
                    }

                    Exception exception = null;
                    var result = default(TResult);
                    try
                    {
                        result = await ItemAction.Invoke(item.Argument);
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }

                    lock (Synchronizer)
                    {
                        _processingItems.Remove(item);
                        item.NotifyAboutCompletion(result, item.Cancelled, exception);
                    }
                }
            });
        }

        private QueueItem<TArgument, TResult> DequeueItem()
        {
            QueueItem<TArgument, TResult> item = null;
            lock (Synchronizer)
            {
                if (_processingItems.Count < MaxThreadsCount &&
                    _queue.Count > 0)
                {
                    item = _queue.First().Value;
                    if (item != null)
                    {
                        _queue.Remove(item.FullId);
                        _processingItems.Add(item);
                        item.Status = ExecutionStatus.InProgress;
                    }
                }
            }

            return item;
        }
    }
}
