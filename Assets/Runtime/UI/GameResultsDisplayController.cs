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
        [SerializeField] private PlayerButton player1Button;
        [SerializeField] private PlayerButton player2Button;
        [SerializeField] private PlayerButton player3Button;
        [SerializeField] private PlayerButton player4Button;

        private void Awake() => ValidateRequiredVariables();

        private void OnEnable() => ShowGameResults();

        private void ShowGameResults()
        {
            DisplayTitle();
            DisplayScore();
            DisplayDuration();
            DisplayPlayers();
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



        private void ValidateRequiredVariables()
        {
            if (title == null) { Debug.LogError("Null References: " + title.name); }
            if (scoreA == null) { Debug.LogError("Null References: " + scoreA.name); }
            if (scoreB == null) { Debug.LogError("Null References: " + scoreB.name); }
            if (gameDuration == null) { Debug.LogError("Null References: " + gameDuration.name); }
            if (player1Button == null) { Debug.LogError("Null References: " + player1Button.name); }
            if (player2Button == null) { Debug.LogError("Null References: " + player2Button.name); }
            if (player3Button == null) { Debug.LogError("Null References: " + player3Button.name); }
            if (player4Button == null) { Debug.LogError("Null References: " + player4Button.name); }
        }
    }
}