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
    }
}