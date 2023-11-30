using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Foni.Code.InputSystem;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using Foni.Code.Util;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace Foni.Code.PhoneticsSystem
{
    public enum ELetterState
    {
        Default,
        Incorrect,
        Correct
    }

    [Serializable]
    public class LetterStateConfig
    {
        public ELetterState state;
        public Color color;
        public GameObject[] enabledObjects;
    }

    public class LetterComponent : MonoBehaviour, IInputListener
    {
        [Header("References")] //
        [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField] private SpriteRenderer altImageRenderer;

        [SerializeField] private SpriteRenderer innerCircle;
        [SerializeField] private GameObject animateRoot;

        [Header("Config")] //
        [SerializeField]
        private List<LetterStateConfig> states;

        [Header("Config/Animation")] //
        [SerializeField]
        private EEasing showEasing;

        [SerializeField] private EEasing hideEasing;

        [SerializeField] private float showDuration;
        [SerializeField] private float hideDuration;

        [SerializeField] private float flutterAnimTime;
        [SerializeField] private float flutterAnimIntervalDelay;
        [SerializeField] private AnimationCurve flutterSizeAnimCurve;
        private Coroutine _activeFlutterAnim;
        private Vector3 _initialScale;


        public Letter Letter { get; private set; }

        public event EventHandler OnClicked;

        private void Start()
        {
            _initialScale = animateRoot.transform.localScale;
        }

        public void UpdateFromLetter(Letter newLetter, Random randomness)
        {
            Letter = newLetter;

            var useAltImage = newLetter.AltImage?.Asset != null && randomness.Next(2) == 0;

            text.enabled = !useAltImage;
            text.SetText(newLetter.ID);

            altImageRenderer.enabled = useAltImage;
            altImageRenderer.sprite = newLetter.AltImage?.Asset;
        }

        public void HideImmediately()
        {
            animateRoot.transform.localScale = Vector3.zero;
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
        }

        public void StartFlutter()
        {
            if (_activeFlutterAnim != null)
            {
                StopCoroutine(_activeFlutterAnim);
            }

            _activeFlutterAnim = StartCoroutine(DoFlutterLoop());
        }

        public void StopFlutter()
        {
            if (_activeFlutterAnim == null) return;

            StopCoroutine(_activeFlutterAnim);
        }

        public void ClearFlutterScale()
        {
            animateRoot.transform.localScale = _initialScale;
        }

        private IEnumerator DoFlutterLoop()
        {
            while (true)
            {
                var time = 0.0f;
                var preFlutterScale = animateRoot.transform.localScale;
                while (time < flutterAnimTime)
                {
                    var value = flutterSizeAnimCurve.Evaluate(time / flutterAnimTime);
                    animateRoot.transform.localScale =
                        new Vector3(value * preFlutterScale.x, value * preFlutterScale.y, value * preFlutterScale.z);
                    time += Time.deltaTime;
                    yield return null;
                }

                animateRoot.transform.localScale = preFlutterScale;

                yield return new WaitForSeconds(flutterAnimIntervalDelay);
            }
        }

        public void SetState(ELetterState newState)
        {
            DisableAllStateObjects();

            var matchingNewStateConfig = states.Find(state => state.state == newState);

            Debug.AssertFormat(matchingNewStateConfig != null, "No matching state config for state {0}", newState);

            matchingNewStateConfig.enabledObjects
                .ToList()
                .ForEach(GameObjectUtils.EnableIfDisabled);

            SetInnerCircleColor(matchingNewStateConfig.color);
            StopFlutter();
        }

        private void SetInnerCircleColor(Color newColor)
        {
            innerCircle.color = newColor;
        }

        private void DisableAllStateObjects()
        {
            states.SelectMany(state => state.enabledObjects)
                .ToList()
                .ForEach(GameObjectUtils.DisableIfEnabled);
        }

        void IInputListener.OnPressDown()
        {
            OnClicked?.Invoke(this, EventArgs.Empty);
        }

        void IInputListener.OnPressUp()
        {
        }
    }
}