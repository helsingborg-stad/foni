using Foni.Code.DebugUtils;
using TMPro;
using UnityEngine;

namespace Foni.Code.UI
{
    public class DebugHUD : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private TextMeshProUGUI fpsText;

        [SerializeField] private FPSCounterComponent fpsCounterComponent;

        private void Start()
        {
            fpsCounterComponent.StartCalculatingFPS();
        }

        private void Update()
        {
            fpsText.SetText($"fps: {1.0f / Time.deltaTime:00.} ({fpsCounterComponent.RollingAverageFPS:00.})");
        }
    }
}