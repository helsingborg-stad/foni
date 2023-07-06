using UnityEngine;

namespace Foni.Code.TweenSystem.Actions
{
    public static partial class TweenAction
    {
        public static TickEvent RectTransformSizeDeltaX(GameObject target)
        {
            var rectTransform = target.transform as RectTransform;
            Debug.AssertFormat(rectTransform != null, "TransformAnchoredPositionX rectTransform is null");
            return interpolatedValue =>
            {
                var size = rectTransform.sizeDelta;
                rectTransform.sizeDelta = new Vector2(
                    interpolatedValue, size.y);
            };
        }
    }
}