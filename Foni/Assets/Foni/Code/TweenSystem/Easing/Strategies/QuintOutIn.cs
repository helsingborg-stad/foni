using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float QuintOutIn(float normalizedTime, float from, float to)
        {
            return normalizedTime < 0.5f
                ? QuintOut(normalizedTime * 2, from, Mathf.Lerp(from, to, 0.5f))
                : QuintIn((normalizedTime - 0.5f) * 2, Mathf.Lerp(from, to, 0.5f), to);
        }
    }
}