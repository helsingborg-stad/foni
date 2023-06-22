using System.Collections;
using UnityEngine;

namespace Foni.Code.DebugUtils
{
    public class FPSCounterComponent : MonoBehaviour
    {
        [SerializeField] private float averagingTimeSeconds = 2.0f;

        private float _accumulatedTime;
        private int _frameCount;

        public float RollingAverageFPS { get; private set; }

        public void StartCalculatingFPS()
        {
            RollingAverageFPS = 0f;
            _accumulatedTime = 0f;
            _frameCount = 0;

            // Start the coroutine to update the FPS value
            StartCoroutine(UpdateFPS());
        }

        private IEnumerator UpdateFPS()
        {
            while (true)
            {
                // Calculate the average FPS over the specified time
                if (_accumulatedTime >= averagingTimeSeconds)
                {
                    RollingAverageFPS = _frameCount / averagingTimeSeconds;
                    _accumulatedTime = 0f;
                    _frameCount = 0;
                }

                // Accumulate the time and frame count
                _accumulatedTime += Time.deltaTime;
                _frameCount++;

                yield return null;
            }
        }
    }
}