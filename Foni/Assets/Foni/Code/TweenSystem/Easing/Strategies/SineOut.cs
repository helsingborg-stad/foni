using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float SineOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * Mathf.Sin(normalizedTime * (Mathf.PI / 2));
        }
    }
}