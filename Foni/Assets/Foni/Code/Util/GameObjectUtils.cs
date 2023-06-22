using UnityEngine;

namespace Foni.Code.Util
{
    public static class GameObjectUtils
    {
        public static void DisableIfEnabled(GameObject gameObject)
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }

        public static void EnableIfDisabled(GameObject gameObject)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }

        public static GameObject GetGameObject(MonoBehaviour behaviourSource)
        {
            return behaviourSource.gameObject;
        }

        public static void DestroyAllChildren(this GameObject parent)
        {
            var childCount = parent.transform.childCount;

            for (var i = childCount - 1; i >= 0; i--)
            {
                var childObject = parent.transform.GetChild(i).gameObject;
                childObject.transform.SetParent(null);
                Object.Destroy(childObject);
            }
        }
    }
}