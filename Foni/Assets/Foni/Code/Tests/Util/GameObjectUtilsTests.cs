using Foni.Code.Util;
using NUnit.Framework;
using UnityEngine;

namespace Foni.Code.Tests.Util
{
    public class GameObjectUtilsTests
    {
        [Test]
        public void DisableIfEnabled()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(true);

            Assert.IsTrue(gameObject.activeSelf);

            gameObject.DisableIfEnabled();

            Assert.IsFalse(gameObject.activeSelf);
        }

        [Test]
        public void EnabledIfDisabled()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(false);

            Assert.IsFalse(gameObject.activeSelf);

            gameObject.EnableIfDisabled();

            Assert.IsTrue(gameObject.activeSelf);
        }

        private class MockComponent : MonoBehaviour
        {
        }

        [Test]
        public void GetGameObject()
        {
            var gameObject = new GameObject();
            var behaviour = gameObject.AddComponent<MockComponent>();

            var result = behaviour.GetGameObject();

            Assert.AreEqual(gameObject, result);
        }

        [Test]
        public void DestroyAllChildren()
        {
            var root = new GameObject();
            var child1 = new GameObject();
            var child2 = new GameObject();
            child1.transform.SetParent(root.transform);
            child2.transform.SetParent(root.transform);

            var valueBefore = root.transform.childCount;
            root.DestroyAllChildren();
            // ReSharper disable once Unity.InefficientPropertyAccess
            var valueAfter = root.transform.childCount;

            Assert.AreEqual(2, valueBefore);
            Assert.Zero(valueAfter);
        }
    }
}