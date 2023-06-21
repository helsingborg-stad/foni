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

            GameObjectUtils.DisableIfEnabled(gameObject);

            Assert.IsFalse(gameObject.activeSelf);
        }

        [Test]
        public void EnabledIfDisabled()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(false);

            Assert.IsFalse(gameObject.activeSelf);

            GameObjectUtils.EnableIfDisabled(gameObject);

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

            var result = GameObjectUtils.GetGameObject(behaviour);

            Assert.AreEqual(gameObject, result);
        }
    }
}