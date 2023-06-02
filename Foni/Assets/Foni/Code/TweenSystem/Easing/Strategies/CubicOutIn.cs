using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float CubicOutIn(float normalizedTime, float from, float to)
        {
            return normalizedTime < 0.5f
                ? CubicOut(normalizedTime * 2, from, Mathf.Lerp(from, to, 0.5f))
                : CubicIn((normalizedTime - 0.5f) * 2, Mathf.Lerp(from, to, 0.5f), to);
        }
    }
}