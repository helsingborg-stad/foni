using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float CircIn(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (1 - Mathf.Sqrt(1 - normalizedTime * normalizedTime));
        }
    }
}