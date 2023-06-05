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

        public void SetFromLetters(List<Letter> letters)
        {
            Assert.IsTrue(letters.Count == letterComponents.Count);

            for (var i = 0; i < letterComponents.Count; i++)
            {
                letterComponents[i].UpdateFromLetter(letters[i]);
            }
        }
    }
}