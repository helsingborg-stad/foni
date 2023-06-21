using UnityEngine;
using UnityEngine.SceneManagement;

namespace Foni.Code.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void OnClick_Start()
        {
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
    }
}