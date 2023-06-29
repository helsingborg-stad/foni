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

        public void OnPressDown()
        {
            StopAnimation();
            _activeAnimationCoroutine = StartCoroutine(TweenManager.OneShot(pressDownEasing, 1.0f, pressedScale,
                pressDownDuration, TweenAction.TransformScale(gameObject)));
        }

        public void OnPressUp()
        {
            StopAnimation();
            _activeAnimationCoroutine = StartCoroutine(TweenManager.OneShot(pressUpEasing, pressedScale, 1.0f,
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