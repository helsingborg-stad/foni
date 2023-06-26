using UnityEngine;

namespace Foni.Code.TweenSystem.Actions
{
    public static partial class TweenAction
    {
        public static TickEvent TransformPositionY(GameObject target)
        {
            return y =>
            {
                var position = target.transform.position;
                position = new Vector3(
                    position.x,
                    y,
                    position.z
                );
                target.transform.position = position;
            };
        }

        public static TickEvent TransformPositionY(MonoBehaviour target)
        {
            return TransformScale(target.gameObject);
        }
    }
}