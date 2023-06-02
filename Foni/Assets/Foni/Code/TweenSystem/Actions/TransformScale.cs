using UnityEngine;

namespace Foni.Code.TweenSystem.Actions
{
    public static partial class TweenAction
    {
        public static TickEvent TransformScale(GameObject target)
        {
            return scale => { target.transform.localScale = new Vector3(scale, scale, scale); };
        }

        public static TickEvent TransformScale(MonoBehaviour target)
        {
            return TransformScale(target.gameObject);
        }
    }
}