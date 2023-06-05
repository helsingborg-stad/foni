using System;
using System.Collections.Generic;
using System.Linq;
using Foni.Code.InputSystem;
using Foni.Code.Util;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [Header("References")] [SerializeField]
        private TextMeshProUGUI text;

        [SerializeField] private SpriteRenderer innerCircle;

        [Header("Config")] [SerializeField] private List<LetterStateConfig> states;

        public void UpdateFromLetter(Letter newLetter)
        {
            text.SetText(newLetter.ID);
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
            var randomState = (ELetterState)Random.Range(0, 3);
            SetState(randomState);
        }

        void IInputListener.OnPressUp()
        {
        }
    }
}