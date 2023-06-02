using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ExpoIn(float normalizedTime, float from, float to)
        {
            return from + (to - from) *
                ((normalizedTime <= 0.0f) ? (normalizedTime) : (Mathf.Pow(2, 10 * (normalizedTime - 1))));
        }
    }
}