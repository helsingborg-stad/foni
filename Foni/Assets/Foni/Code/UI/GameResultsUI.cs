using System.Collections;
using System.Collections.Generic;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using Foni.Code.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = System.Diagnostics.Debug;

namespace Foni.Code.UI
{
    public class GameResultsUI : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private GameObject animateRoot;

        [SerializeField] private GameObject cardRoot;

        [SerializeField] private ResultCardWidget resultCardPrefab;

        [Header("Config/Animation")] //
        [SerializeField]
        private EEasing showEasing;

        [SerializeField] private float showDuration;

        private void Start()
        {
            gameObject.DisableIfEnabled();
        }

        public IEnumerator AnimateShow()
        {
            var rect = animateRoot.transform as RectTransform;
            Debug.Assert(rect != null, "animateRoot.transform is not RectTransform");

            var width = Screen.width;
            yield return TweenManager.OneShot(showEasing, width, 0.0f, showDuration,
                TweenAction.TransformAnchoredPositionX(animateRoot));
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

        public void OnClick_Home()
        {
            SceneManager.LoadScene("ProfileMenuScene", LoadSceneMode.Single);
        }
    }
}