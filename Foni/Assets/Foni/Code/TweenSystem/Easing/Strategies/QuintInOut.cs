using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float QuintInOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * ((normalizedTime < 0.5f)
                ? (16 * normalizedTime * normalizedTime * normalizedTime * normalizedTime * normalizedTime)
                : (0.5f * ((2 * normalizedTime) - 2) * ((2 * normalizedTime) - 2) * ((2 * normalizedTime) - 2) *
                    ((2 * normalizedTime) - 2) * ((2 * normalizedTime) - 2) + 1));
        }
    }
}