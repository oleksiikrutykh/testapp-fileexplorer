using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.FileExplorer.ImageLoading
{
    class QueueItem<TArgument, TResult>
    {
        private int _priority;

        private bool _finished;

        public QueueItem(int priority,
                         TArgument argument)
        {
            _priority = priority;
            Argument = argument;
        }

        internal void AssociateWithQueue(PriorityQueue<TArgument, TResult> owner, int id)
        {
            Owner = owner;
            Id = id;
            CalculateFullId();
        }

        internal PriorityQueue<TArgument, TResult> Owner { get; private set; }

        public int Id { get; private set; }

        public bool Cancelled { get; internal set; }

        public int Priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    if (Owner != null)
                    {
                        Owner.ChangePriority(this, value);
                    }
                    else
                    {
                        _priority = value;
                    }
                }
            }
        }

        internal void UpdatePriority(int newPriority)
        {
            _priority = newPriority;
            CalculateFullId();
        }

        public long FullId { get; private set; }

        public TArgument Argument { get; private set; }

        public ExecutionStatus Status { get; internal set; }

        public void Cancel()
        {
            Owner.Cancel(this);
        }

        internal void NotifyAboutCompletion(TResult result, bool cancelled, Exception ex)
        {
            if (_finished)
            {
                return;
            }

            _finished = true;
            if (cancelled)
            {
                Status = ExecutionStatus.Cancelled;
                _taskSource.SetCanceled();
                return;
            }

            if (ex != null)
            {
                Status = ExecutionStatus.Faulted;
                _taskSource.SetException(ex);
                return;
            }

            Status = ExecutionStatus.Completed;
            _taskSource.SetResult(result);
        }

        private TaskCompletionSource<TResult> _taskSource = new TaskCompletionSource<TResult>();

        public Task<TResult> WhenCompleted()
        {
            return _taskSource.Task;
        }

        private void CalculateFullId()
        {
            FullId = ((long)(int.MaxValue - Priority - 1)) * int.MaxValue + Id;
        }
    }
}
