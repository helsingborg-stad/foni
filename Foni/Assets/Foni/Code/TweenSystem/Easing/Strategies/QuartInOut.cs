using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float QuartInOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * ((normalizedTime < 0.5f)
                ? (8 * normalizedTime * normalizedTime * normalizedTime * normalizedTime)
                : (-8 * (normalizedTime - 1) * (normalizedTime - 1) * (normalizedTime - 1) * (normalizedTime - 1) + 1));
        }
    }
}