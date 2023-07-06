using Foni.Code.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Foni.Code.UI
{
    public class GameHUD : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private ProfileInfoWidget profileInfoWidget;

        private void Start()
        {
            var profile = Globals.ServiceLocator.ProfileService.GetActiveProfile();
            if (profile.HasValue)
            {
                profileInfoWidget.SetFromProfile(profile.Value);
            }
        }

        public void OnClick_Home()
        {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }
    }
}