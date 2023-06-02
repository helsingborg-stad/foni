using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        public static float QuadOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (-(normalizedTime * (normalizedTime - 2)));
        }
    }
}