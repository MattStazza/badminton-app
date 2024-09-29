using UnityEngine;
using TMPro;

namespace Runtime.UI
{
    public class ScoreDisplayController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI gameDuration;
        [SerializeField] private TextMeshProUGUI teamAScore;
        [SerializeField] private TextMeshProUGUI teamBScore;
        [Space]
        [SerializeField] private GameObject scoreDisplay;
        [SerializeField] private GameObject scoreButtons;
        [Space]
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject backButton;

        private void Awake() => ValidateRequiredVariables();

        public void SetTitle(string text) => title.text = text;

        public void UpdateScoreDisplay(int scoreA, int scoreB)
        {
            teamAScore.text = scoreA.ToString();
            teamBScore.text = scoreB.ToString();
        }

        public void DisplayGamePreview()
        {
            HideAllUIElements();

            startButton.SetActive(true);
            backButton.SetActive(true);
        }

        public void DisplayGameScoring()
        {
            HideAllUIElements();

            SetTitle("SCORE");
            scoreDisplay.SetActive(true);
            scoreButtons.SetActive(true);
            gameDuration.gameObject.SetActive(true);
        }

        private void HideAllUIElements()
        {
            scoreDisplay.SetActive(false);
            scoreButtons.SetActive(false);
            gameDuration.gameObject.SetActive(false);
            startButton.SetActive(false);
            backButton.SetActive(false);
        }

        


        private void ValidateRequiredVariables()
        {
            if (title == null) { Debug.LogError("Null References: " + title.name); }
            if (gameDuration == null) { Debug.LogError("Null References: " + gameDuration.name); }

            if (scoreDisplay == null) { Debug.LogError("Null References: " + scoreDisplay.name); }
            if (teamAScore == null) { Debug.LogError("Null References: " + teamAScore.name); }
            if (teamBScore == null) { Debug.LogError("Null References: " + teamBScore.name); }
            if (scoreButtons == null) { Debug.LogError("Null References: " + scoreButtons.name); }

            if (startButton == null) { Debug.LogError("Null References: " + startButton.name); }
            if (backButton == null) { Debug.LogError("Null References: " + backButton.name); }
        }
    }
}


