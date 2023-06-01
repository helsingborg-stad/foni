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

        public static TComponent GetChecked<TComponent>(MonoBehaviour source)
            where TComponent : MonoBehaviour
        {
            var hopefullyTheComponent = source.GetComponent<TComponent>();
            Debug.AssertFormat(hopefullyTheComponent != null, "No component of type {0} on Game Mode object!",
                typeof(TComponent));
            return hopefullyTheComponent;
        }
    }
}