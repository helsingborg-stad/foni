using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float QuartOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) *
                ((normalizedTime - 1) * (normalizedTime - 1) * (normalizedTime - 1) * (1 - normalizedTime) + 1);
        }
    }
}