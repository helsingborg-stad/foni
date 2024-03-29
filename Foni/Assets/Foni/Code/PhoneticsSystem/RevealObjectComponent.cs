using System.Collections;
using Foni.Code.Core;
using Foni.Code.InputSystem;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Easing;
using UnityEngine;
using UnityEngine.Assertions;
using TweenAction = Foni.Code.TweenSystem.Actions.TweenAction;

namespace Foni.Code.PhoneticsSystem
{
    public class RevealObjectComponent : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private Renderer animateRenderer;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private WorldSpaceButtonComponent spriteButton;

        [Header("Config/Animation")] //
        [SerializeField]
        private EEasing showEasing;

        [SerializeField] private EEasing hideEasing;
        [SerializeField] private float showDuration;
        [SerializeField] private float hideDuration;

        [SerializeField] private EEasing revealEasing;
        [SerializeField] private float revealDuration;

        [SerializeField] private EEasing bounceEasing1;
        [SerializeField] private EEasing bounceEasing2;
        [SerializeField] private float bounceScale;
        [SerializeField] private float bounceDuration;

        private Material _materialInstance;
        private AudioClip _relatedAudioClip;
        private static readonly int RevealPropertyId = Shader.PropertyToID("_Reveal");

        public delegate void SoundButtonClickedEvent();

        public SoundButtonClickedEvent OnSoundButtonClickedEvent;

        private void Start()
        {
            animateRoot.transform.localScale = Vector3.zero;
            spriteButton.enabled = false;
            SetupShaderForAnimation();
            spriteButton.OnClicked = TryPlaySound;
        }

        private void TryPlaySound()
        {
            PlaySound();
            OnSoundButtonClickedEvent.Invoke();
        }

        private void SetupShaderForAnimation()
        {
            Assert.IsNotNull(animateRenderer);
            _materialInstance = new Material(animateRenderer.sharedMaterial);
            animateRenderer.material = _materialInstance;
        }

        public void SetSprite(Sprite newSprite)
        {
            spriteRenderer.sprite = newSprite;
        }

        public void SetSound(AudioClip newClip)
        {
            _relatedAudioClip = newClip;
            newClip.LoadAudioData();
        }

        public IEnumerator AnimateShow()
        {
            yield return TweenManager.OneShot(showEasing, 0.0f, 1.0f, showDuration,
                TweenAction.TransformScale(animateRoot));
        }

        public IEnumerator AnimateHide()
        {
            spriteButton.enabled = false;
            yield return TweenManager.OneShot(hideEasing, 1.0f, 0.0f, hideDuration,
                TweenAction.TransformScale(animateRoot));
        }

        public IEnumerator AnimateReveal()
        {
            var bounceAnimation = StartCoroutine(AnimateBounce());
            var materialAnimation = StartCoroutine(AnimateMaterialReveal());

            yield return bounceAnimation;
            yield return materialAnimation;

            spriteButton.enabled = true;
        }

        public void ShroudImmediately()
        {
            _materialInstance.SetFloat(RevealPropertyId, 0);
        }

        private void PlaySound()
        {
            Globals.AudioManager.PlayAudioOneShot("guess", _relatedAudioClip, false);
        }

        private IEnumerator AnimateMaterialReveal()
        {
            return TweenManager.OneShot(revealEasing, 0.0f, 1.0f, revealDuration,
                TweenAction.SetMaterialFloat(_materialInstance, RevealPropertyId));
        }

        private IEnumerator AnimateBounce()
        {
            var scaleTweenAction = TweenAction.TransformScale(animateRoot);
            yield return TweenManager.OneShot(bounceEasing1, 1.0f, bounceScale, bounceDuration * 0.5f,
                scaleTweenAction);
            yield return TweenManager.OneShot(bounceEasing2, bounceScale, 1.0f, bounceDuration * 0.5f,
                scaleTweenAction);
        }
    }
}