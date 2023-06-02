using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float BackOut(float normalizedTime, float from, float to)
        {
            return from + (to - from) * (1 - ((1 - normalizedTime) * (1 - normalizedTime) * (1 - normalizedTime) -
                                              (1 - normalizedTime) * Mathf.Sin((1 - normalizedTime) * Mathf.PI)));
        }
    }
}