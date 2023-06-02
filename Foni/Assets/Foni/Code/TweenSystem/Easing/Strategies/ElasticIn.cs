using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ElasticIn(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (Mathf.Sin(13 * (Mathf.PI / 2) * normalizedTime) *
                                         Mathf.Pow(2, 10 * (normalizedTime - 1)));
        }
    }
}