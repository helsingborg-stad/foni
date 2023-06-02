using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Foni.Code.AsyncSystem
{
    public interface IAsyncService
    {
        Coroutine StartCoroutine(IEnumerator routine);
        Task RunOnMainThread(Func<IEnumerator> routine);
        void QueueOnMainThread(Action action);
    }
}