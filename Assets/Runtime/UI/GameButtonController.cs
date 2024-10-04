using TMPro;
using UnityEngine;
using Runtime.Data;
using Runtime.Managers;

namespace Runtime.UI
{
    public class GameButtonController : MonoBehaviour
    {
        [SerializeField] private RectTransform gameRectTransform;
        [Space]
        [SerializeField] private TextMeshProUGUI gameNumberText;
        [Space]
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private PlayerButtonController playerOneButton;
        [SerializeField] private PlayerButtonController playerTwoButton;
        [SerializeField] private PlayerButtonController playerThreeButton;
        [SerializeField] private PlayerButtonController playerFourButton;
        [Space]
        [SerializeField] private GameObject completedGamePanel;
        [SerializeField] private TextMeshProUGUI teamAScore;
        [SerializeField] private TextMeshProUGUI teamBScore;

        private ScoringManager scoringManager;
        private Game gameData;

        private void Awake() => ValidateRequiredVariables();

        public void SetGameData(Game game) => gameData = game;
        public void SetGameNumber(int number) => gameNumberText.text = "GAME #" + number.ToString();

        public void SelectGame()
        {
            Session.CurrentGame = gameData;
            scoringManager.SetCurrentGameButton(this);
            scoringManager.OpenGame();
        }

        public void SetTeams(Player player1, Player player2, Player player3, Player player4)
        {
            SetPlayer(playerOneButton, player1);
            SetPlayer(playerTwoButton, player2);
            SetPlayer(playerThreeButton, player3);
            SetPlayer(playerFourButton, player4);
        }

        public void ToggleGamePlayed(bool played)
        {
            gamePanel.SetActive(!played);
            completedGamePanel.SetActive(played);

            if (played)
            {
                gameRectTransform.sizeDelta = new Vector2(700f, 500f);
                teamAScore.text = gameData.ScoreA.ToString();
                teamBScore.text = gameData.ScoreB.ToString();
            }
            else
                gameRectTransform.sizeDelta = new Vector2(700f, 1000);
        }

        private void SetPlayer(PlayerButtonController playerButton, Player player)
        {
            PlayerData playerData = new PlayerData
            {
                playerName = player.Name,
                profileTexture = player.ProfileSprite,
                bodyTexture = player.BodySprite
            };

            playerButton.SetupPlayerButton(playerData);
            playerButton.ToggleButtonInteractive(false);
        }

        private void ValidateRequiredVariables()
        {
            scoringManager = FindObjectOfType<ScoringManager>();
            if (scoringManager == null) { Debug.LogError("Null References: " + scoringManager.name); }

            if (gameRectTransform == null) { Debug.LogError("Null References: " + gameRectTransform.name); }
            if (gameNumberText == null) { Debug.LogError("Null References: " + gameNumberText.name); }
            if (gamePanel == null) { Debug.LogError("Null References: " + gamePanel.name); }
            if (playerOneButton == null) { Debug.LogError("Null References: " + playerOneButton.name); }
            if (playerTwoButton == null) { Debug.LogError("Null References: " + playerTwoButton.name); }
            if (playerThreeButton == null) { Debug.LogError("Null References: " + playerThreeButton.name); }
            if (playerFourButton == null) { Debug.LogError("Null References: " + playerFourButton.name); }
            if (completedGamePanel == null) { Debug.LogError("Null References: " + completedGamePanel.name); }
            if (teamAScore == null) { Debug.LogError("Null References: " + teamAScore.name); }
            if (teamBScore == null) { Debug.LogError("Null References: " + teamBScore.name); }
        }
    }
}

