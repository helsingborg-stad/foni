using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ElasticOutIn(float normalizedTime, float from, float to)
        {
            return normalizedTime < 0.5f
                ? ElasticOut(normalizedTime * 2, from, Mathf.Lerp(from, to, 0.5f))
                : ElasticIn((normalizedTime - 0.5f) * 2, Mathf.Lerp(from, to, 0.5f), to);
        }
    }
}