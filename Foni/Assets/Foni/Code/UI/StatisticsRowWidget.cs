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
            var swedishDateTimeOffset = TimeZoneInfo.ConvertTime(DateTimeOffset
                .Parse(session.timestampStart)
                .ToLocalTime(), TimeZoneInfo.FindSystemTimeZoneById("Europe/Stockholm"));
            var dateString = swedishDateTimeOffset.ToString("yyyy-MM-dd HH:mm:ss");

            dateText.SetText(dateString);

            var newDurationSpan = TimeSpan.FromSeconds(session.totalSessionTimeS);
            durationText.SetText($"{newDurationSpan.Minutes}m {newDurationSpan.Seconds}s");

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