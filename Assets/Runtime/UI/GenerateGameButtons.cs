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
        [SerializeField] private int numberOfGamesToGenerate;   // TODO: Make this customisable on the Setup Page with a slider?
        [Space]
        [SerializeField] private GameObject buttons;

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


        private void SpawnGameButton(int gameNumber)
        {
            GameObject gameButton = Instantiate(gameButtonPrefab, transform);
            GameButton gameButtonController = gameButton.GetComponent<GameButton>();
            gameButtons.Add(gameButtonController);

            UpdateGameButtonWithSessionData(gameButtonController, gameNumber);

            buttons.transform.SetAsLastSibling();
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


        private void ValidateRequiredVariables()
        {
            if (gameButtonPrefab == null) { Debug.LogError("Null References: " + gameButtonPrefab.name); }
            if (buttons == null) { Debug.LogError("Null References: " + buttons.name); }
        }
    }
}


