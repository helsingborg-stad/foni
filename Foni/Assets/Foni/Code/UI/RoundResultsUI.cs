using System.Collections;
using System.Collections.Generic;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using Foni.Code.Util;
using UnityEngine;

namespace Foni.Code.UI
{
    public class RoundResultsUI : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private GameObject cardRoot;

        [SerializeField] private ResultCardWidget resultCardPrefab;

        [Header("Config/Animation")] //
        [SerializeField]
        private EEasing showEasing;

        [SerializeField] private EEasing hideEasing;

        [SerializeField] private float showDuration;
        [SerializeField] private float hideDuration;

        public delegate void ContinueEvent();

        public ContinueEvent OnContinue;

        private void Start()
        {
            gameObject.DisableIfEnabled();
        }

        public IEnumerator AnimateShow()
        {
            gameObject.EnableIfDisabled();
            yield return TweenManager.OneShot(showEasing, 0.0f, 1.0f, showDuration,
                TweenAction.TransformScale(animateRoot));
        }

        public void SetCards(List<ResultCardInfo> cards)
        {
            cardRoot.DestroyAllChildren();
            cards.ForEach(CreateCard);
        }

        private void CreateCard(ResultCardInfo info)
        {
            var newCard = Instantiate(resultCardPrefab, cardRoot.transform);
            newCard.Set(info);
        }

        public void OnClick_Continue()
        {
            StartCoroutine(DoHideProcess());
        }

        private IEnumerator DoHideProcess()
        {
            yield return TweenManager.OneShot(hideEasing, 1.0f, 0.0f, hideDuration,
                TweenAction.TransformScale(animateRoot));
            OnContinue.Invoke();
            gameObject.DisableIfEnabled();
        }
    }
}