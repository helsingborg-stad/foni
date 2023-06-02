using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ExpoOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) *
                ((normalizedTime >= 1.0f) ? (normalizedTime) : (1 - Mathf.Pow(2, -10 * normalizedTime)));
        }
    }
}