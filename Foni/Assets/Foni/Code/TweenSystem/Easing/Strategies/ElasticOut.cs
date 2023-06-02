using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ElasticOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (Mathf.Sin(-13 * (Mathf.PI / 2) * (normalizedTime + 1)) *
                Mathf.Pow(2, -10 * normalizedTime) + 1);
        }
    }
}