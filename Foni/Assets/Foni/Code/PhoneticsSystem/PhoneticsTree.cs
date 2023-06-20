using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Foni.Code.PhoneticsSystem
{
    public class PhoneticsTree : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private List<LetterComponent> letterComponents;

        public int RequiredLetterCount => letterComponents.Count;

        public event EventHandler<LetterComponent> OnLeafClicked;

        private void Start()
        {
            letterComponents.ForEach(BindClickEvent);
        }

        public void SetFromLetters(List<Letter> letters)
        {
            Assert.IsTrue(letters.Count == letterComponents.Count);

            for (var i = 0; i < letterComponents.Count; i++)
            {
                letterComponents[i].UpdateFromLetter(letters[i]);
            }
        }

        public void ResetAllLeaves()
        {
            letterComponents.ForEach(ResetLetterComponentVisuals);
        }

        private void ResetLetterComponentVisuals(LetterComponent letterComponent)
        {
            letterComponent.SetState(ELetterState.Default);
        }

        private void BindClickEvent(LetterComponent letterComponent)
        {
            letterComponent.OnClicked += (_, _) => OnLetterComponentClicked(letterComponent);
        }

        private void OnLetterComponentClicked(LetterComponent letterComponent)
        {
            OnLeafClicked?.Invoke(this, letterComponent);
        }
    }
}