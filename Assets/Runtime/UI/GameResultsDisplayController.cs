using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Runtime.Data;


namespace Runtime.UI
{
    public class GameResultsDisplayController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI scoreA;
        [SerializeField] private TextMeshProUGUI scoreB;
        [SerializeField] private TextMeshProUGUI gameDuration;
        [SerializeField] private GameObject backButton;
        [Space]
        [Header ("Players")]
        [SerializeField] private PlayerButton player1Button;
        [SerializeField] private ScoreIcon score1;
        [Space]
        [SerializeField] private PlayerButton player2Button;
        [SerializeField] private ScoreIcon score2;
        [Space]
        [SerializeField] private PlayerButton player3Button;
        [SerializeField] private ScoreIcon score3;
        [Space]
        [SerializeField] private PlayerButton player4Button;
        [SerializeField] private ScoreIcon score4;

        private void Awake() => ValidateRequiredVariables();

        private void OnEnable() => ShowGameResults();

        private void ShowGameResults()
        {
            DisplayTitle();
            DisplayScore();
            DisplayDuration();
            DisplayPlayers();
            DisplayScoreIcons();
            DisplayBackButton();
        }

        private void DisplayTitle()
        {
            title.text = "GAME #" + Session.CurrentGame.Number.ToString() + " RESULTS";
        }

        private void DisplayScore()
        {
            scoreA.text = Session.CurrentGame.ScoreA.ToString();
            scoreB.text = Session.CurrentGame.ScoreB.ToString();
        }

        private void DisplayDuration()
        {
            gameDuration.text = Session.CurrentGame.Duration.ToString();
        }

        private void DisplayPlayers()
        {
            foreach (KeyValuePair<Player, Player> player in Session.CurrentGame.TeamA)
            { SetPlayer(player.Key, player1Button); SetPlayer(player.Value, player2Button); }

            foreach (KeyValuePair<Player, Player> player in Session.CurrentGame.TeamB)
            { SetPlayer(player.Key, player3Button); SetPlayer(player.Value, player4Button); }
        }

        private void SetPlayer(Player player, PlayerButton button)
        {
            PlayerData playerData = new PlayerData
            {
                playerName = player.Name,
                profileTexture = player.ProfileSprite,
                bodyTexture = player.BodySprite
            };

            button.UpdatePlayerButtonDisplay(playerData);
            button.ToggleButtonInteractive(false);
        }

        private void DisplayScoreIcons()
        {
            bool teamAWon = Session.CurrentGame.ScoreA > Session.CurrentGame.ScoreB;
            bool duece = Session.CurrentGame.ScoreA > 21 || Session.CurrentGame.ScoreB > 21;

            score1.DisplayScoreIcon(teamAWon, duece);
            score2.DisplayScoreIcon(teamAWon, duece);
            score3.DisplayScoreIcon(!teamAWon, duece);
            score4.DisplayScoreIcon(!teamAWon, duece);
        }

        private void DisplayBackButton()
        {
            backButton.SetActive(!Session.CurrentGame.Complete);
        }


        private void ValidateRequiredVariables()
        {
            if (title == null) { Debug.LogError("Null References: " + title.name); }
            if (scoreA == null) { Debug.LogError("Null References: " + scoreA.name); }
            if (scoreB == null) { Debug.LogError("Null References: " + scoreB.name); }
            if (gameDuration == null) { Debug.LogError("Null References: " + gameDuration.name); }
            if (backButton == null) { Debug.LogError("Null References: " + backButton.name); }
            if (player1Button == null) { Debug.LogError("Null References: " + player1Button.name); }
            if (player2Button == null) { Debug.LogError("Null References: " + player2Button.name); }
            if (player3Button == null) { Debug.LogError("Null References: " + player3Button.name); }
            if (player4Button == null) { Debug.LogError("Null References: " + player4Button.name); }
            if (score1 == null) { Debug.LogError("Null References: " + score1.name); }
            if (score2 == null) { Debug.LogError("Null References: " + score2.name); }
            if (score3 == null) { Debug.LogError("Null References: " + score3.name); }
            if (score4 == null) { Debug.LogError("Null References: " + score4.name); }
        }
    }
}