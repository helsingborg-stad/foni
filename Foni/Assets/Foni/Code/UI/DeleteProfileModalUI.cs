using System.Collections;
using Foni.Code.AsyncSystem;
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
    public class DeleteProfileModalUI : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private TextMeshProUGUI nameText;

        [Header("Animation")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private Image backdropImage;
        [SerializeField] private float finalBackdropOpacity;

        [SerializeField] private EEasing showEasing;

        [SerializeField] private float showDuration;
        [SerializeField] private EEasing hideEasing;
        [SerializeField] private float hideDuration;

        public delegate void DeleteActiveProfileEvent();

        public DeleteActiveProfileEvent OnDeleteActiveProfile;

        private void Start()
        {
            HideImmediately();
        }

        public void SetFromProfile(ProfileData profile)
        {
            nameText.SetText(profile.name);
        }

        public void ConfirmDeleteActiveProfile()
        {
            var activeProfile = Globals.ServiceLocator.ProfileService.GetActiveProfile();
            if (activeProfile.HasValue)
            {
                StartCoroutine(DoDeleteProfile(activeProfile.Value.name));
            }
        }

        private IEnumerator DoDeleteProfile(string profileId)
        {
            yield return new WaitForTask(() =>
                Globals.ServiceLocator.ProfileService.RemoveProfile(profileId));
            Hide();
            OnDeleteActiveProfile.Invoke();
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