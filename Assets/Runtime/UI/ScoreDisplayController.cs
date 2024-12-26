using UnityEngine;
using TMPro;

namespace Runtime.UI
{
    public class ScoreDisplayController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameDuration;
        [SerializeField] private TextMeshProUGUI teamAScore;
        [SerializeField] private TextMeshProUGUI teamBScore;
        [Space]
        [SerializeField] private GameObject scoreDisplay;
        [SerializeField] private GameObject scoreButtons;

        private void Awake() => ValidateRequiredVariables();

        public void UpdateScoreDisplay(int scoreA, int scoreB)
        {
            teamAScore.text = scoreA.ToString();
            teamBScore.text = scoreB.ToString();
        }

        // SEPERATE GAME PREVIEW AND SCORING INTO 2 PAGES!?

        /*public void DisplayGamePreview()
        {
            HideAllUIElements();

            swapButton.SetActive(true);
            startButton.SetActive(true);
            backButton.SetActive(true);
        }

        public void DisplayGameScoring()
        {
            HideAllUIElements();

            SetTitle(" "); // Hide Title Instead?
            scoreDisplay.SetActive(true);
            scoreButtons.SetActive(true);
            gameDuration.gameObject.SetActive(true);
        }

        private void HideAllUIElements()
        {
            swapButton.SetActive(false);
            scoreDisplay.SetActive(false);
            scoreButtons.SetActive(false);
            gameDuration.gameObject.SetActive(false);
            startButton.SetActive(false);
            backButton.SetActive(false);
        }*/

        


        private void ValidateRequiredVariables()
        {
            if (gameDuration == null) { Debug.LogError("Null References: " + gameDuration.name); }
            if (scoreDisplay == null) { Debug.LogError("Null References: " + scoreDisplay.name); }
            if (teamAScore == null) { Debug.LogError("Null References: " + teamAScore.name); }
            if (teamBScore == null) { Debug.LogError("Null References: " + teamBScore.name); }
            if (scoreButtons == null) { Debug.LogError("Null References: " + scoreButtons.name); }
        }
    }
}


