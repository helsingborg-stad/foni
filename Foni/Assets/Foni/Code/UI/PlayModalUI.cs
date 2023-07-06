using System.Collections;
using Foni.Code.Core;
using Foni.Code.ProfileSystem;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Foni.Code.UI
{
    public class PlayModalUI : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField] private Image avatarImage;

        [Header("Animation")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private Image backdropImage;
        [SerializeField] private float finalBackdropOpacity;

        [SerializeField] private EEasing showEasing;

        [SerializeField] private float showDuration;
        [SerializeField] private EEasing hideEasing;
        [SerializeField] private float hideDuration;

        public delegate void StartGameEvent();

        public StartGameEvent OnStartGame;

        private void Start()
        {
            HideImmediately();
        }

        public void StartGame()
        {
            OnStartGame?.Invoke();
        }

        public void SetFromProfile(ProfileData profile)
        {
            nameText.SetText(profile.name);
            avatarImage.sprite = Globals.AvatarIconMapper.GetSprite(profile.icon);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            StartCoroutine(DoShow());
        }

        public void Hide()
        {
            StartCoroutine(DoHide());
        }

        private void HideImmediately()
        {
            gameObject.SetActive(false);
            var backdropColor = backdropImage.color;
            backdropImage.color = new Color(backdropColor.r, backdropColor.g, backdropColor.b, 0);
            var rect = animateRoot.transform as RectTransform;
            if (rect != null) rect.anchoredPosition = new Vector2(3000.0f, rect.anchoredPosition.y);
        }

        private IEnumerator DoShow()
        {
            var backdropAnimation = StartCoroutine(TweenManager.OneShot(showEasing, 0.0f, finalBackdropOpacity,
                showDuration,
                TweenAction.SetImageOpacity(backdropImage)));
            var contentAnimation = StartCoroutine(TweenManager.OneShot(showEasing, 3000.0f, 0.0f, showDuration,
                TweenAction.TransformAnchoredPositionX(animateRoot)));

            yield return backdropAnimation;
            yield return contentAnimation;
        }

        private IEnumerator DoHide()
        {
            var backdropAnimation = StartCoroutine(TweenManager.OneShot(hideEasing, finalBackdropOpacity, 0.0f,
                hideDuration,
                TweenAction.SetImageOpacity(backdropImage)));
            var contentAnimation = StartCoroutine(TweenManager.OneShot(hideEasing, 0.0f, 3000.0f, hideDuration,
                TweenAction.TransformAnchoredPositionX(animateRoot)));

            yield return backdropAnimation;
            yield return contentAnimation;
            gameObject.SetActive(false);
        }
    }
}