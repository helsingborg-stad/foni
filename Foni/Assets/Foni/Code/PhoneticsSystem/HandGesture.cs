using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using UnityEngine;

namespace Foni.Code.PhoneticsSystem
{
    public class HandGesture : MonoBehaviour
    {
        private enum EState
        {
            Shown,
            Hidden
        }

        [Header("References")] //
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [Header("Config/Animation")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private Vector2 toFromYValue;
        [SerializeField] private EEasing showEasing;
        [SerializeField] private float showDuration;
        [SerializeField] private EEasing hideEasing;
        [SerializeField] private float hideDuration;

        private EState _state;
        private Coroutine _activeCoroutine;

        private void Start()
        {
            _state = EState.Hidden;
        }

        public void SetSprite(Sprite newSprite)
        {
            spriteRenderer.sprite = newSprite;
        }

        private void StopActiveCoroutine()
        {
            if (_activeCoroutine == null) return;
            StopCoroutine(_activeCoroutine);
            _activeCoroutine = null;
        }

        public void Show()
        {
            if (_state != EState.Hidden) return;
            StopActiveCoroutine();

            _state = EState.Shown;
            _activeCoroutine = StartCoroutine(TweenManager.OneShot(showEasing, animateRoot.transform.localPosition.y,
                toFromYValue.y,
                showDuration,
                TweenAction.TransformPositionY(animateRoot)));
        }

        public void Hide()
        {
            if (_state != EState.Shown) return;
            StopActiveCoroutine();

            _state = EState.Hidden;
            _activeCoroutine = StartCoroutine(TweenManager.OneShot(hideEasing, animateRoot.transform.localPosition.y,
                toFromYValue.x,
                hideDuration,
                TweenAction.TransformPositionY(animateRoot)));
        }
    }
}