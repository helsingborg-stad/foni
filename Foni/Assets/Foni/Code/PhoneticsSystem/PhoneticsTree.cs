using System;
using System.Collections;
using System.Collections.Generic;
using Foni.Code.Util;
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
            letterComponents.ForEach(ResetLetterComponentVisuals);
            letterComponents.ForEach(HideLetterComponentVisuals);
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

        public IEnumerator AnimateShowingLeaves()
        {
            var coroutines = new List<Coroutine>();
            var componentsInRandomizedOrder = letterComponents.InRandomOrder();

            foreach (var letterComponent in componentsInRandomizedOrder)
            {
                coroutines.Add(StartCoroutine(letterComponent.AnimateShow()));
                yield return new WaitForSeconds(0.1f);
            }

            foreach (var coroutine in coroutines)
            {
                yield return coroutine;
            }
        }

        public IEnumerator AnimateHidingLeaves()
        {
            var coroutines = new List<Coroutine>();
            var componentsInRandomizedOrder = letterComponents.InRandomOrder();

            foreach (var letterComponent in componentsInRandomizedOrder)
            {
                coroutines.Add(StartCoroutine(letterComponent.AnimateHide()));
                yield return new WaitForSeconds(0.1f);
            }

            foreach (var coroutine in coroutines)
            {
                yield return coroutine;
            }
        }

        private static void ResetLetterComponentVisuals(LetterComponent letterComponent)
        {
            letterComponent.SetState(ELetterState.Default);
        }

        private static void HideLetterComponentVisuals(LetterComponent letterComponent)
        {
            letterComponent.HideImmediately();
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