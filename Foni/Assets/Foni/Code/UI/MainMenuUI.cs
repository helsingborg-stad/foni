using System.Collections;
using Foni.Code.AsyncSystem;
using Foni.Code.SystemPropertiesSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Foni.Code.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI versionText;

        private IEnumerator Start()
        {
            versionText.text = "...";
            ISystemPropertiesService systemPropertiesService = null;
            yield return new WaitForTask<ISystemPropertiesService>(SystemPropertiesServiceFactory.Create,
                sps => systemPropertiesService = sps);

            if (systemPropertiesService != null)
            {
                versionText.SetText(
                    $"v{systemPropertiesService.Version}.{systemPropertiesService.Build}.{systemPropertiesService.Revision}");
            }
        }

        public void OnClick_Start()
        {
            SceneManager.LoadScene("ProfileMenuScene", LoadSceneMode.Single);
        }
    }
}