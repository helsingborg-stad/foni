using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float QuadInOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * ((normalizedTime < 0.5f)
                ? (2 * normalizedTime * normalizedTime)
                : ((-2 * normalizedTime * normalizedTime) + (4 * normalizedTime) - 1));
        }
    }
}