using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ElasticInOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (
                (normalizedTime < 0.5f)
                    ? (0.5f * Mathf.Sin(13 * (Mathf.PI / 2) * (2 * normalizedTime)) *
                       Mathf.Pow(2, 10 * ((2 * normalizedTime) - 1)))
                    : (0.5f * (Mathf.Sin(-13 * (Mathf.PI / 2) * ((2 * normalizedTime - 1) + 1)) *
                            Mathf.Pow(2, -10 * (2 * normalizedTime - 1)) + 2)
                    ));
        }
    }
}