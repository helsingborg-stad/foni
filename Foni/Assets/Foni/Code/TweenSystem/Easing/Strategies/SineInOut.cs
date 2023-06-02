using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float SineInOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (0.5f * (1 - Mathf.Cos(normalizedTime * Mathf.PI)));
        }
    }
}