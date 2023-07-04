using UnityEngine;
using UnityEngine.UI;

namespace Foni.Code.UI
{
    public class AvatarIconWidget : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private Button button;

        [SerializeField] private Image image;
        [SerializeField] private GameObject highlightRoot;

        public delegate void OnClickEvent(string spriteName);

        public OnClickEvent OnClick;

        private void Start()
        {
            SetHighlighted(false);
            button.onClick.AddListener(() => { OnClick?.Invoke(image.sprite.name); });
        }

        public void SetFromSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void SetHighlighted(bool newHighlighted)
        {
            highlightRoot.SetActive(newHighlighted);
        }
    }
}