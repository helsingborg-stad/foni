using System;
using System.Collections;
using System.Threading.Tasks;
using Foni.Code.Core;
using UnityEngine;

namespace Foni.Code.AsyncSystem
{
    public static class AwaitCoroutine
    {
        private static IEnumerator WithCallback(YieldInstruction coroutine, Action callback)
        {
            yield return coroutine;
            callback();
        }

        public static Task Run(Func<YieldInstruction> action)
        {
            var taskRunner = new TaskCompletionSource<bool>();
            Globals.ServiceLocator.AsyncService.StartCoroutine(WithCallback(action(),
                () => { taskRunner.SetResult(true); }));
            return taskRunner.Task;
        }
    }
}