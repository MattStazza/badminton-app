using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        private UIManager uiManager;
        private GenerateGameButtons generator;

        private void Awake() => ValidateRequiredVariables();

        public void SetGameNumber(int number)
        {
            gameNumberText.text = "GAME #" + number.ToString();
        }
        public void SetTeams(Player player1, Player player2, Player player3, Player player4)
        {
            SetPlayer(playerOneButton, player1);
            SetPlayer(playerTwoButton, player2);
            SetPlayer(playerThreeButton, player3);
            SetPlayer(playerFourButton, player4);
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

        public void ToggleGamePlayed(bool played)
        {
            gamePanel.SetActive(!played);
            completedGamePanel.SetActive(played);

            if (played)
                gameRectTransform.sizeDelta = new Vector2(700f, 500f);
            else
                gameRectTransform.sizeDelta = new Vector2(700f, 1000);

            generator.UpdateContentHeight();
        }

        public void OpenGameScene()
        {
            uiManager.ShowScoringPage();
        }




        private void ValidateRequiredVariables()
        {
            uiManager = FindObjectOfType<UIManager>();
            if (uiManager == null) { Debug.LogError("Null References: " + uiManager.name); }

            generator = FindObjectOfType<GenerateGameButtons>();
            if (generator == null) { Debug.LogError("Null References: " + generator.name); }

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

