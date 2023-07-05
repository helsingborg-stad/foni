using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Foni.Code.Core;
using Foni.Code.ProfileSystem;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using Foni.Code.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Foni.Code.UI
{
    public class AddProfileUI : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private Button acceptButton;

        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TextMeshProUGUI errorText;
        [SerializeField] private RectTransform avatarIconRoot;
        [SerializeField] private AvatarIconWidget avatarIconPrefab;
        [SerializeField] private AvatarIconMapper avatarIconMapper;

        [Header("Animation")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private Image backdropImage;
        [SerializeField] private float finalBackdropOpacity;

        [SerializeField] private EEasing showEasing;

        [SerializeField] private float showDuration;
        [SerializeField] private EEasing hideEasing;
        [SerializeField] private float hideDuration;

        private ProfileData _profileData;
        private Dictionary<string, AvatarIconWidget> _iconNameWidgetMap;

        public delegate void SubmitProfileEvent(ProfileData newProfile);

        public SubmitProfileEvent OnSubmitProfile;

        private void Start()
        {
            _iconNameWidgetMap = new Dictionary<string, AvatarIconWidget>();
            nameField.onValueChanged.AddListener(OnNameChanged);
            HideImmediately();
        }

        public void SubmitProfile()
        {
            OnSubmitProfile?.Invoke(_profileData);
            acceptButton.interactable = false;
            Hide();
        }

        public void Show()
        {
            _profileData = new ProfileData
            {
                name = null,
                icon = null,
                statistics = new StatisticsData()
            };
            gameObject.SetActive(true);
            errorText.text = "";
            acceptButton.interactable = false;
            SetupAvatarIcons();
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

        private void SetupAvatarIcons()
        {
            avatarIconRoot.gameObject.DestroyAllChildren();

            _iconNameWidgetMap.Clear();
            var sprites = avatarIconMapper.GetSprites();
            sprites.ForEach(CreateAvatarIcon);
        }

        private void UpdateAcceptButtonState()
        {
            acceptButton.interactable =
                _profileData is { name: not null, icon: not null } &&
                !Globals.ServiceLocator.ProfileService.ContainsProfile(_profileData.name);
        }

        private void CreateAvatarIcon(Sprite sprite)
        {
            var newAvatarIcon = Instantiate(avatarIconPrefab, avatarIconRoot);
            newAvatarIcon.SetFromSprite(sprite);
            newAvatarIcon.OnClick += SelectAvatarIcon;
            _iconNameWidgetMap.Add(sprite.name, newAvatarIcon);
        }

        private void SelectAvatarIcon(string iconName)
        {
            _profileData.icon = iconName;

            _iconNameWidgetMap.Values
                .ToList()
                .ForEach(widget => widget.SetHighlighted(false));

            _iconNameWidgetMap[iconName].SetHighlighted(true);
            UpdateAcceptButtonState();
        }

        private void OnNameChanged(string newName)
        {
            _profileData.name = null;
            if (newName.Length == 0)
            {
                errorText.text = "Inget namn angivet";
            }
            else
            {
                var profileAlreadyExists = Globals.ServiceLocator.ProfileService.ContainsProfile(newName);
                if (profileAlreadyExists)
                {
                    errorText.text = "Profil med samma namn finns redan";
                }
                else
                {
                    errorText.text = "";
                    _profileData.name = newName;
                }
            }

            UpdateAcceptButtonState();
        }
    }
}