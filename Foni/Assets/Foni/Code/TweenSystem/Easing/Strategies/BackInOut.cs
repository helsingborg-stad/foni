using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float BackInOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * ((normalizedTime < 0.5f)
                ? (0.5f * ((2 * normalizedTime) * (2 * normalizedTime) * (2 * normalizedTime) -
                           (2 * normalizedTime) * Mathf.Sin((2 * normalizedTime) * Mathf.PI)))
                : (0.5f * (1 -
                           ((1 - (2 * normalizedTime - 1)) * (1 - (2 * normalizedTime - 1)) *
                               (1 - (2 * normalizedTime - 1)) - (1 - (2 * normalizedTime - 1)) *
                               Mathf.Sin((1 - (2 * normalizedTime - 1)) * Mathf.PI))) + 0.5f));
        }
    }
}