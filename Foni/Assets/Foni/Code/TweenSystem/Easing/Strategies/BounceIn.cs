using UnityEngine;

namespace Foni.Code.TweenSystem.Easing
{
    public static partial class StrategyFactory
    {
        private static float BounceIn(float normalizedTime, float from, float to)
        {
            float p = (1 - normalizedTime);
            float x;
            if (p < 4 / 11.0f)
            {
                x = (121 * p * p) / 16.0f;
            }
            else if (p < 8 / 11.0f)
            {
                x = (363 / 40.0f * p * p) - (99 / 10.0f * p) + 17 / 5.0f;
            }
            else if (p < 9 / 10.0f)
            {
                x = (4356 / 361.0f * p * p) - (35442 / 1805.0f * p) + 16061 / 1805.0f;
            }
            else
            {
                x = (54 / 5.0f * p * p) - (513 / 25.0f * p) + 268 / 25.0f;
            }

            return from + (to - from) * (1 - (1 - x));
        }
    }
}