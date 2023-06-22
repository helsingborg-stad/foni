using UnityEngine;
using UnityEngine.SceneManagement;

namespace Foni.Code.UI
{
    public class GameHUD : MonoBehaviour
    {
        public void OnClick_Home()
        {
            SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
        }
    }
}