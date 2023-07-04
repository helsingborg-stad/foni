using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Foni.Code.UI
{
    public class AvatarWidget : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private Image avatarImage;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Button button;

        public delegate void ClickEvent();

        public ClickEvent OnClick;

        private void Start()
        {
            button.onClick.AddListener(() => OnClick.Invoke());
        }

        public void SetAvatarSprite(Sprite newSprite)
        {
            avatarImage.sprite = newSprite;
        }

        public void SetName(string newName)
        {
            nameText.SetText(newName);
        }
    }
}