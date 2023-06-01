using System;
using System.Threading.Tasks;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Foni.Code.AsyncSystem
{
    public class WaitForTask : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                if (!Task.IsFaulted)
                {
                    return !Task.IsCompleted;
                }

                Debug.Assert(Task.Exception != null, "Task.Exception != null");
                throw Task.Exception;
            }
        }

        private Task Task { get; }

        public WaitForTask(Func<Task> action)
        {
            Task = Task.Run(action);
        }
    }

    public class WaitForTask<T> : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                if (Task.IsFaulted)
                {
                    Debug.Assert(Task.Exception != null, "Task.Exception != null");
                    throw Task.Exception;
                }

                if (!Task.IsCompleted)
                {
                    return true;
                }

                Callback?.Invoke(Task.Result);
                return false;
            }
        }

        private Action<T> Callback { get; }
        private Task<T> Task { get; }

        public WaitForTask(Func<Task<T>> action, Action<T> callback = null)
        {
            Callback = callback;
            Task = System.Threading.Tasks.Task.Run(action);
        }
    }
}