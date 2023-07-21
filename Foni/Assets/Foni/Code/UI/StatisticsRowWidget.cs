using System;
using Foni.Code.ProfileSystem;
using Foni.Code.Util;
using TMPro;
using UnityEngine;

namespace Foni.Code.UI
{
    public class StatisticsRowWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dateText;
        [SerializeField] private TextMeshProUGUI durationText;
        [SerializeField] private Transform guessesRoot;
        [SerializeField] private StatisticsGuessWidget guessWidgetPrefab;

        public void SetFromSession(SessionData session)
        {
            dateText.SetText(
                DateTimeUtils.DateTimeToFriendlyString(
                    DateTimeUtils.TimestampToDateTime(session.timestampStart)));

            durationText.SetText(
                DateTimeUtils.TimeSpanToFriendlyString(
                    TimeSpan.FromSeconds(session.totalSessionTimeS)));

            guessesRoot.gameObject.DestroyAllChildren();
            session.guesses.ForEach(CreateGuessWidget);
        }

        private void CreateGuessWidget(SingleGuessData guess)
        {
            var widget = Instantiate(guessWidgetPrefab, guessesRoot);
            widget.SetFromGuess(guess);
        }
    }
}