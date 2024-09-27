using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class GenerateGameButtons : MonoBehaviour
    {
        [SerializeField] private GameObject gameButtonPrefab;
        [SerializeField] private int numberOfGamesToGenerate;
        [Space]
        [SerializeField] private RectTransform contentRectTransform;
        [SerializeField] private VerticalLayoutGroup contentVerticalLayout;

        private List<GameButtonController> gameButtons = new List<GameButtonController>();


        private void Awake() => ValidateRequiredVariables();
        private void Start() => GenerateButtons();



        private void GenerateButtons()
        {
            for (int g = 0; g < numberOfGamesToGenerate; g++)
            {
                // Spawn Game Buttons
                GameObject gameButton = Instantiate(gameButtonPrefab, transform);
                GameButtonController gameButtonController = gameButton.GetComponent<GameButtonController>();
                gameButtons.Add(gameButtonController);

                // Update Button Design with Session Data
                UpdateGameButtonWithSessionData(gameButtonController, g);
            }

            UpdateContentHeight();
            RefreshContentLayout();
        }




        public void AddOneGame()
        {
            GameObject gameButton = Instantiate(gameButtonPrefab, transform);
            GameButtonController gameButtonController = gameButton.GetComponent<GameButtonController>();
            gameButtons.Add(gameButtonController);

            UpdateGameButtonWithSessionData(gameButtonController, gameButtons.Count - 1);

            UpdateContentHeight();
            RefreshContentLayout();
        }


        private void UpdateGameButtonWithSessionData(GameButtonController gameButton ,int gameNumber)
        {
            gameButton.SetGameNumber(gameNumber + 1);

            Player playerOne = null;
            Player playerTwo = null;
            Player playerThree = null;
            Player playerFour = null;

            foreach (KeyValuePair<Player, Player> player in Session.Games[gameNumber].TeamA)
            { playerOne = player.Key; playerTwo = player.Value; }

            foreach (KeyValuePair<Player, Player> player in Session.Games[gameNumber].TeamB)
            { playerThree = player.Key; playerFour = player.Value; }

            gameButton.SetTeams(playerOne, playerTwo, playerThree, playerFour);
        }


        private void UpdateContentHeight()
        {
            float CONTENT_WDITH = 900f;
            float gameHeight = 1000f; // Dynamically get button height for each game (different for played or not)

            float contentHeight = (gameHeight * gameButtons.Count) + (contentVerticalLayout.spacing * gameButtons.Count);

            contentRectTransform.sizeDelta = new Vector2(CONTENT_WDITH, contentHeight);
        }

        private void RefreshContentLayout()
        {
            contentVerticalLayout.enabled = false;
            contentVerticalLayout.enabled = true;
        }




        private void ValidateRequiredVariables()
        {
            if (gameButtonPrefab == null) { Debug.LogError("Null References: " + gameButtonPrefab.name); }
            if (contentRectTransform == null) { Debug.LogError("Null References: " + contentRectTransform.name); }
            if (contentVerticalLayout == null) { Debug.LogError("Null References: " + contentVerticalLayout.name); }
        }
    }
}


