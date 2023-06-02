using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float BackIn(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (normalizedTime * normalizedTime * normalizedTime -
                                         normalizedTime * Mathf.Sin(normalizedTime * Mathf.PI));
        }
    }
}