using System;
using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float ExpoInOut(float normalizedTime, float from, float to)
        {
            if (Math.Abs(normalizedTime - 1.0f) < 0.001f || normalizedTime == 0.0f) return normalizedTime;
            return from + (to - from) *
                (normalizedTime < 0.5f
                    ? 0.5f * Mathf.Pow(2, 20 * normalizedTime - 10)
                    : -0.5f * Mathf.Pow(2, -20 * normalizedTime + 10) + 1
                );
        }
    }
}