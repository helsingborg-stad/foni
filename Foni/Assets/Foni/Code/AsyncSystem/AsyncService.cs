using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Foni.Code.ServicesSystem;
using UnityEngine;

namespace Foni.Code.AsyncSystem
{
    public class AsyncService : MonoBehaviour, IService, IAsyncService
    {
        private struct MainThreadTask
        {
            public Func<IEnumerator> Routine;
            public Action Callback;
        }

        private readonly Queue<MainThreadTask> _pendingMainThreadTasks = new();
        private readonly Queue<Action> _pendingMainThreadActions = new();

        private static IEnumerator RunMainThreadTask(MainThreadTask task)
        {
            yield return task.Routine.Invoke();
            task.Callback();
        }

        private void Update()
        {
            RunQueuedMainThreadTasks();
            RunQueuedMainThreadActions();
        }

        private void RunQueuedMainThreadTasks()
        {
            lock (_pendingMainThreadTasks)
            {
                while (_pendingMainThreadTasks.TryDequeue(out var pendingTask))
                {
                    StartCoroutine(RunMainThreadTask(pendingTask));
                }
            }
        }

        private void RunQueuedMainThreadActions()
        {
            lock (_pendingMainThreadTasks)
            {
                while (_pendingMainThreadActions.TryDequeue(out var pendingAction))
                {
                    pendingAction();
                }
            }
        }

        public Task RunOnMainThread(Func<IEnumerator> routine)
        {
            var tcs = new TaskCompletionSource<bool>();

            var mainThreadTask = new MainThreadTask
            {
                Routine = routine,
                Callback = () => { tcs.SetResult(true); }
            };

            lock (_pendingMainThreadTasks)
            {
                _pendingMainThreadTasks.Enqueue(mainThreadTask);
            }

            return tcs.Task;
        }

        public void QueueOnMainThread(Action action)
        {
            lock (_pendingMainThreadActions)
            {
                _pendingMainThreadActions.Enqueue(action);
            }
        }
    }
}