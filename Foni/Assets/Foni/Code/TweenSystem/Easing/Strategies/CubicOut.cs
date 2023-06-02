using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float CubicOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * ((normalizedTime - 1) * (normalizedTime - 1) * (normalizedTime - 1) + 1);
        }
    }
}