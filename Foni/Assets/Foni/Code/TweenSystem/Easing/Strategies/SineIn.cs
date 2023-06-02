using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float SineIn(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (Mathf.Sin((normalizedTime - 1) * (Mathf.PI / 2)) + 1);
        }
    }
}