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

            Assert.True(gameObject.activeSelf);

            GameObjectUtils.DisableIfEnabled(gameObject);

            Assert.False(gameObject.activeSelf);
        }

        [Test]
        public void EnabledIfDisabled()
        {
            var gameObject = new GameObject();
            gameObject.SetActive(false);

            Assert.False(gameObject.activeSelf);

            GameObjectUtils.EnableIfDisabled(gameObject);

            Assert.True(gameObject.activeSelf);
        }
    }
}