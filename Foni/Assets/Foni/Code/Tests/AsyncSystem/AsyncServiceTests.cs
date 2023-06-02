using System;
using System.Collections;
using Foni.Code.AsyncSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;

namespace Foni.Code.Tests.AsyncSystem
{
    public class AsyncServiceTests
    {
        private static IAsyncService CreateAsyncService()
        {
            var gameObject = new GameObject();
            return gameObject.AddComponent<AsyncService>();
        }

        [UnityTest]
        public IEnumerator StartCoroutine_Runs()
        {
            var asyncService = CreateAsyncService();
            var shouldBeTrue = false;

            IEnumerator MockCoroutine()
            {
                yield return null;
                shouldBeTrue = true;
            }

            asyncService.StartCoroutine(MockCoroutine());
            yield return null;

            Assert.True(shouldBeTrue);
        }

        [UnityTest]
        public IEnumerator CallOnMainThread_Runs()
        {
            var asyncService = CreateAsyncService();
            var mainThreadId = Environment.CurrentManagedThreadId;
            var taskThreadId = -1;
            var taskMainThreadId = -1;

            yield return new WaitForTask(() =>
            {
                var tsc = new TaskCompletionSource<bool>();
                taskThreadId = Environment.CurrentManagedThreadId;
                asyncService.QueueOnMainThread(() =>
                {
                    taskMainThreadId = Environment.CurrentManagedThreadId;
                    tsc.SetResult(true);
                });
                return tsc.Task;
            });

            Assert.AreNotEqual(mainThreadId, taskThreadId);
            Assert.AreEqual(mainThreadId, taskMainThreadId);
        }
    }
}