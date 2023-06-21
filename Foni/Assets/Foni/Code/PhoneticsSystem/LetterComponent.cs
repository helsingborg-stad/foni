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


        public Letter Letter { get; private set; }

        public event EventHandler OnClicked;

        public void UpdateFromLetter(Letter newLetter)
        {
            Letter = newLetter;
            text.SetText(newLetter.ID);
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

        public void SetState(ELetterState newState)
        {
            DisableAllStateObjects();

            var matchingNewStateConfig = states.Find(state => state.state == newState);

            Debug.AssertFormat(matchingNewStateConfig != null, "No matching state config for state {0}", newState);

            matchingNewStateConfig.enabledObjects
                .ToList()
                .ForEach(GameObjectUtils.EnableIfDisabled);

            SetInnerCircleColor(matchingNewStateConfig.color);
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