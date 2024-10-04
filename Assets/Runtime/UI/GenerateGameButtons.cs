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

        private List<GameButton> gameButtons = new List<GameButton>();

        private void Awake() => ValidateRequiredVariables();
        private void Start() => GenerateButtons();

        private void GenerateButtons()
        {
            for (int g = 0; g < numberOfGamesToGenerate; g++)
            {
                if (g >= Session.Games.Count)
                {
                    Debug.Log("No more unique games! Adding previously played games.");
                    return;
                }

                SpawnGameButton(g);
            }
        }

        public void AddOneGame()
        {
            if (gameButtons.Count >= Session.Games.Count)
            {
                Debug.Log("No more unique games! Adding previously played games.");
                return;
            }

            SpawnGameButton(gameButtons.Count);
        }

        public void UpdateContentHeight()
        {
            float allGameButtonsHeight = 0;

            foreach (GameButton gameButton in gameButtons)
                allGameButtonsHeight = allGameButtonsHeight + gameButton.gameObject.GetComponent<RectTransform>().sizeDelta.y;

            float contentHeight = allGameButtonsHeight + (contentVerticalLayout.spacing * gameButtons.Count);

            contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, contentHeight);

            RefreshContentLayout();
        }

        private void SpawnGameButton(int gameNumber)
        {
            GameObject gameButton = Instantiate(gameButtonPrefab, transform);
            GameButton gameButtonController = gameButton.GetComponent<GameButton>();
            gameButtons.Add(gameButtonController);

            UpdateGameButtonWithSessionData(gameButtonController, gameNumber);

            UpdateContentHeight();
            RefreshContentLayout();
        }

        private void UpdateGameButtonWithSessionData(GameButton gameButton ,int gameNumber)
        {
            gameButton.SetGameData(Session.Games[gameNumber]);

            Session.Games[gameNumber].Number = gameNumber + 1;
            gameButton.SetGameNumber(Session.Games[gameNumber].Number);

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


