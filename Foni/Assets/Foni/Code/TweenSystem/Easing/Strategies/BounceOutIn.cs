using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float BounceOutIn(float normalizedTime, float from, float to)
        {
            return normalizedTime < 0.5f
                ? BounceOut(normalizedTime * 2, from, Mathf.Lerp(from, to, 0.5f))
                : BounceIn((normalizedTime - 0.5f) * 2, Mathf.Lerp(from, to, 0.5f), to);
        }
    }
}