using System.Collections;
using Foni.Code.AsyncSystem;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
        public IEnumerator RunsCoroutine()
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
    }
}