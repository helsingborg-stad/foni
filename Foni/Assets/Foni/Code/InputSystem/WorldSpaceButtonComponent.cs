using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using UnityEngine;

namespace Foni.Code.InputSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class WorldSpaceButtonComponent : MonoBehaviour, IInputListener
    {
        [Header("Config/Animation")] //
        [SerializeField]
        private float pressedScale;

        [SerializeField] private EEasing pressDownEasing;
        [SerializeField] private float pressDownDuration;
        [SerializeField] private EEasing pressUpEasing;
        [SerializeField] private float pressUpDuration;

        public delegate void ClickEvent();

        public ClickEvent OnClicked;

        private Coroutine _activeAnimationCoroutine;

        private float _animStartScale;

        private void Start()
        {
            _animStartScale = gameObject.transform.localScale.x;
        }

        public void OnPressDown()
        {
            if (!enabled) return;

            StopAnimation();
            _activeAnimationCoroutine = StartCoroutine(TweenManager.OneShot(pressDownEasing, _animStartScale,
                pressedScale,
                pressDownDuration, TweenAction.TransformScale(gameObject)));
        }

        public void OnPressUp()
        {
            if (!enabled) return;

            StopAnimation();
            _activeAnimationCoroutine = StartCoroutine(TweenManager.OneShot(pressUpEasing, pressedScale,
                _animStartScale,
                pressUpDuration, TweenAction.TransformScale(gameObject)));

            OnClicked();
        }

        private void StopAnimation()
        {
            if (_activeAnimationCoroutine == null) return;
            StopCoroutine(_activeAnimationCoroutine);
            _activeAnimationCoroutine = null;
        }
    }
}