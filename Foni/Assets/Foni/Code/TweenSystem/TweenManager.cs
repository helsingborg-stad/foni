using System.Collections;
using Foni.Code.TweenSystem.Easing;
using UnityEngine;

namespace Foni.Code.TweenSystem
{
    public delegate void TickEvent(float interpolatedValue);

    public class TweenManager : MonoBehaviour
    {
        public static IEnumerator OneShot(EEasing easingType, float from, float to, float duration, TickEvent onTick)
        {
            var easeFunc = StrategyFactory.GetStrategy(easingType);
            var elapsedTime = 0.0f;

            while (elapsedTime < duration)
            {
                var normalizedTime = elapsedTime / duration;
                var interpolatedValue = easeFunc(normalizedTime, from, to);
                onTick(interpolatedValue);
                yield return null;
                elapsedTime += Time.deltaTime;
            }

            onTick(to);
        }
    }
}