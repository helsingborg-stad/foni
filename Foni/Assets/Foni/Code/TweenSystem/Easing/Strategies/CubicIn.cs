using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float CubicIn(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (normalizedTime * normalizedTime * normalizedTime);
        }
    }
}