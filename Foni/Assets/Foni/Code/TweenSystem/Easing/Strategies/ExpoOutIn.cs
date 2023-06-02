using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ExpoOutIn(float normalizedTime, float from, float to)
        {
            return normalizedTime < 0.5f
                ? ExpoOut(normalizedTime * 2, from, Mathf.Lerp(from, to, 0.5f))
                : ExpoIn((normalizedTime - 0.5f) * 2, Mathf.Lerp(from, to, 0.5f), to);
        }
    }
}