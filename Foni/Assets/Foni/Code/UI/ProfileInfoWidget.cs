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
    public class ProfileInfoWidget : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField] private Image avatarImage;

        [Header("Animation")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private EEasing showEasing;
        [SerializeField] private float showDuration;
        [SerializeField] private EEasing hideEasing;
        [SerializeField] private float hideDuration;

        private bool _isShowing;

        private void Start()
        {
            HideImmediately();
        }

        public void SetFromProfile(ProfileData profileData)
        {
            nameText.SetText(profileData.name);
            avatarImage.sprite = Globals.AvatarIconMapper.GetSprite(profileData.icon);
        }

        private void HideImmediately()
        {
            var rectTransform = animateRoot.transform as RectTransform;
            if (rectTransform != null)
            {
                rectTransform.localScale = Vector3.zero;

                //rectTransform.sizeDelta = new Vector2(0, rectTransform.sizeDelta.y);
            }

            _isShowing = false;
        }

        public void Toggle()
        {
            if (_isShowing)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        public void Show()
        {
            StartCoroutine(DoShow());
        }

        private IEnumerator DoShow()
        {
            _isShowing = true;
            yield return TweenManager.OneShot(showEasing, 0, 1, showDuration,
                TweenAction.TransformScale(animateRoot));
        }

        public void Hide()
        {
            StartCoroutine(DoHide());
        }

        private IEnumerator DoHide()
        {
            _isShowing = false;
            yield return TweenManager.OneShot(hideEasing, 1, 0, hideDuration,
                TweenAction.TransformScale(animateRoot));
        }
    }
}