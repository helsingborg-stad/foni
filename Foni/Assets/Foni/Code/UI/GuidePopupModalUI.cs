using System.Collections;
using Foni.Code.AsyncSystem;
using Foni.Code.Core;
using Foni.Code.TweenSystem;
using Foni.Code.TweenSystem.Actions;
using Foni.Code.TweenSystem.Easing;
using Foni.Code.Util;
using UnityEngine;

namespace Foni.Code.UI
{
    public class GuidePopupModalUI : MonoBehaviour
    {
        [SerializeField] private GameObject animateRoot;
        [SerializeField] private EEasing animateShowEasing;
        [SerializeField] private EEasing animateHideEasing;
        [SerializeField] private float animateShowDuration;
        [SerializeField] private float animateHideDuration;

        private void Start()
        {
            StartCoroutine(DoViewCheck());
        }

        public void HideAndSetAsViewed()
        {
            StartCoroutine(DoHideAndSetAsViewed());
        }

        public void Show()
        {
            gameObject.EnableIfDisabled();
            StartCoroutine(DoShow());
        }

        private IEnumerator DoHideAndSetAsViewed()
        {
            yield return TweenManager.OneShot(animateHideEasing, 1.0f, 0.0f, animateHideDuration,
                TweenAction.TransformScale(animateRoot));
            yield return new WaitForTask(() => Globals.ServiceLocator.SaveService.Save("hasViewedGuide", "true"));
            gameObject.DisableIfEnabled();
        }

        private IEnumerator DoShow()
        {
            gameObject.EnableIfDisabled();
            yield return TweenManager.OneShot(animateShowEasing, 0.0f, 1.0f, animateShowDuration,
                TweenAction.TransformScale(animateRoot));
        }

        private IEnumerator DoViewCheck()
        {
            animateRoot.transform.localScale = Vector3.zero;
            yield return null;
            var hasViewedGuide = false;
            yield return new WaitForTask<string>(
                () => Globals.ServiceLocator.SaveService.LoadOrDefault("hasViewedGuide", "false"),
                content => { bool.TryParse(content, out hasViewedGuide); });

            if (hasViewedGuide)
            {
                gameObject.DisableIfEnabled();
            }
            else
            {
                yield return DoShow();
            }
        }
    }
}