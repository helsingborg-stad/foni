using System.Collections;
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
        [SerializeField] private WorldSpaceButtonComponent soundButton;
        [SerializeField] private GameObject soundButtonAnimateRoot;
        [SerializeField] private AudioSource audioSource;

        [Header("Config/Animation")] //
        [SerializeField]
        private EEasing showEasing;

        [SerializeField] private EEasing hideEasing;
        [SerializeField] private float showDuration;
        [SerializeField] private float hideDuration;

        [SerializeField] private EEasing revealEasing;
        [SerializeField] private float revealDuration;

        [SerializeField] private EEasing soundButtonShowEasing;
        [SerializeField] private float soundButtonShowDuration;

        [SerializeField] private EEasing bounceEasing1;
        [SerializeField] private EEasing bounceEasing2;
        [SerializeField] private float bounceScale;
        [SerializeField] private float bounceDuration;

        private Material _materialInstance;
        private static readonly int RevealPropertyId = Shader.PropertyToID("_Reveal");

        public delegate void SoundButtonClickedEvent();

        public SoundButtonClickedEvent OnSoundButtonClickedEvent;

        private void Start()
        {
            animateRoot.transform.localScale = Vector3.zero;
            soundButtonAnimateRoot.transform.localScale = Vector3.zero;
            SetupShaderForAnimation();
            soundButton.OnClicked = PlaySound;
            soundButton.OnClicked += () => OnSoundButtonClickedEvent.Invoke();
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
            audioSource.clip = newClip;
            newClip.LoadAudioData();
        }

        public IEnumerator AnimateShow()
        {
            yield return TweenManager.OneShot(showEasing, 0.0f, 1.0f, showDuration,
                TweenAction.TransformScale(animateRoot));
        }

        public IEnumerator AnimateHide()
        {
            yield return TweenManager.OneShot(hideEasing, 1.0f, 0.0f, hideDuration,
                TweenAction.TransformScale(animateRoot));
            soundButtonAnimateRoot.transform.localScale = Vector3.zero;
        }

        public IEnumerator AnimateReveal()
        {
            var bounceAnimation = StartCoroutine(AnimateBounce());
            var materialAnimation = StartCoroutine(AnimateMaterialReveal());
            var soundButtonAnimation = StartCoroutine(AnimateSoundButtonShow());

            yield return bounceAnimation;
            yield return materialAnimation;
            yield return soundButtonAnimation;
        }

        private IEnumerator AnimateSoundButtonShow()
        {
            yield return TweenManager.OneShot(soundButtonShowEasing, 0.0f, 1.0f, soundButtonShowDuration,
                TweenAction.TransformScale(soundButtonAnimateRoot));
        }

        public void ShroudImmediately()
        {
            _materialInstance.SetFloat(RevealPropertyId, 0);
        }

        private void PlaySound()
        {
            audioSource.PlayOneShot(audioSource.clip);
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