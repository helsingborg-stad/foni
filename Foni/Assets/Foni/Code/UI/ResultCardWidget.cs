using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Foni.Code.UI
{
    public struct ResultCardInfo
    {
        public string Title;
        public Sprite Sprite;
    }
    
    public class ResultCardWidget : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private Image image;

        [SerializeField] private TextMeshProUGUI text;

        public void Set(ResultCardInfo info)
        {
            image.sprite = info.Sprite;
            text.SetText(info.Title);
        }
    }
}