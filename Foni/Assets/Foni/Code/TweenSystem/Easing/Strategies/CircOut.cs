using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float CircOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * Mathf.Sqrt((2 - normalizedTime) * normalizedTime);
        }
    }
}