using System.Collections;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Easing;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using TweenAction = Foni.Code.TweenSystem.Actions.TweenAction;

namespace Foni.Code.PhoneticsSystem
{
    public class RevealObjectComponent : MonoBehaviour
    {
        [FormerlySerializedAs("revealRoot")]
        [Header("References")] //
        [SerializeField]
        private GameObject animateRoot;

        [FormerlySerializedAs("revealRenderer")] [SerializeField]
        private Renderer animateRenderer;

        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Config/Animation")] //
        [SerializeField]
        private EEasing spawnEasing;

        [SerializeField] private EEasing despawnEasing;

        [SerializeField] private float spawnDuration;

        [SerializeField] private EEasing revealEasing;
        [SerializeField] private float revealDuration;

        [SerializeField] private EEasing bounceEasing1;
        [SerializeField] private EEasing bounceEasing2;
        [SerializeField] private float bounceScale;
        [SerializeField] private float bounceDuration;


        private Material _materialInstance;
        private static readonly int RevealPropertyId = Shader.PropertyToID("_Reveal");

        private void Start()
        {
            animateRoot.transform.localScale = Vector3.zero;
            SetupShaderForAnimation();
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

        public IEnumerator AnimateSpawn()
        {
            yield return TweenManager.OneShot(spawnEasing, 0.0f, 1.0f, spawnDuration,
                TweenAction.TransformScale(animateRoot));
        }

        public IEnumerator AnimateDespawn()
        {
            yield return TweenManager.OneShot(despawnEasing, 1.0f, 0.0f, spawnDuration,
                TweenAction.TransformScale(animateRoot));
        }

        public IEnumerator AnimateReveal()
        {
            var bounceAnimation = StartCoroutine(AnimateBounce());
            var materialAnimation = StartCoroutine(AnimateMaterialReveal());

            yield return bounceAnimation;
            yield return materialAnimation;
        }

        public void HideImmediately()
        {
            _materialInstance.SetFloat(RevealPropertyId, 0);
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