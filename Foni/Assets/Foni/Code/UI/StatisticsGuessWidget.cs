using Foni.Code.ProfileSystem;
using TMPro;
using UnityEngine;

namespace Foni.Code.UI
{
    public class StatisticsGuessWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI letterText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI wrongGuessCountText;
        [SerializeField] private TextMeshProUGUI soundPlayedCountText;
        [SerializeField] private GameObject helpUsedIndicator;

        public void SetFromGuess(SingleGuessData guess)
        {
            letterText.SetText(guess.letter);
            timeText.SetText(guess.durationUntilCorrectS.ToString("F1") + "s");
            wrongGuessCountText.SetText(guess.wrongGuesses.ToString());
            soundPlayedCountText.SetText(guess.timesSoundPlayed.ToString());
            helpUsedIndicator.SetActive(guess.usedHelp);
        }
    }
}