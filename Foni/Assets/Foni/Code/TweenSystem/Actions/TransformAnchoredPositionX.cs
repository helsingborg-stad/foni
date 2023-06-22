using UnityEngine;

namespace Foni.Code.TweenSystem.Actions
{
    public static partial class TweenAction
    {
        public static TickEvent TransformAnchoredPositionX(GameObject target)
        {
            var rectTransform = target.transform as RectTransform;
            Debug.AssertFormat(rectTransform != null, "TransformAnchoredPositionX rectTransform is null");

            return interpolatedValue =>
            {
                var position = rectTransform.anchoredPosition;
                rectTransform.anchoredPosition = new Vector2(
                    interpolatedValue, position.y);
            };
        }

        public static TickEvent TransformAnchoredPositionX(MonoBehaviour target)
        {
            return TransformAnchoredPositionX(target.gameObject);
        }
    }
}