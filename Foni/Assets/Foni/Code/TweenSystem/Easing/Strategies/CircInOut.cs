using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float CircInOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * ((normalizedTime < 0.5f)
                ? (0.5f * (1 - Mathf.Sqrt(1 - 4 * (normalizedTime * normalizedTime))))
                : (0.5f * (Mathf.Sqrt(-((2 * normalizedTime) - 3) * ((2 * normalizedTime) - 1)) + 1)));
        }
    }
}