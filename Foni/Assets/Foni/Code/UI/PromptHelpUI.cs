using System.Collections;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using Foni.Code.Util;
using UnityEngine;

namespace Foni.Code.UI
{
    public class PromptHelpUI : MonoBehaviour
    {
        [Header("References")] //
        [SerializeField]
        private GameObject animateRoot;

        [Header("Config/Animation")] //
        [SerializeField]
        private EEasing showEasing;

        [SerializeField] private EEasing hideEasing;

        [SerializeField] private float showDuration;
        [SerializeField] private float hideDuration;

        public delegate void ContinueEvent(bool acceptedHelp);

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

        public void AcceptHelp()
        {
            StartCoroutine(DoHideProcess(true));
        }

        public void DeclineHelp()
        {
            StartCoroutine(DoHideProcess(false));
        }


        private IEnumerator DoHideProcess(bool acceptedHelp)
        {
            yield return TweenManager.OneShot(hideEasing, 1.0f, 0.0f, hideDuration,
                TweenAction.TransformScale(animateRoot));
            OnContinue.Invoke(acceptedHelp);
            gameObject.DisableIfEnabled();
        }
    }
}