using UnityEngine;
using Runtime.Data;
using Runtime.UI;
using System.Collections.Generic;

namespace Runtime.Managers
{
    public class ScoringManager : MonoBehaviour
    {
        [SerializeField] private UIManager uIManager;
        [SerializeField] private BadmintonCourtManager badmintonCourt;
        [SerializeField] private ScoreDisplayController scoreDisplay;
        [SerializeField] private GenerateGameButtons gameButtonsDisplay;

        private Game currentGameData;
        private GameButtonController currentGameButton;
        private List<Round> rounds = new List<Round>();

        private void Awake() => ValidateRequiredVariables();

        public void SetCurrentGameData(Game game) => currentGameData = game;

        public void SetCurrentGameButton(GameButtonController gameButton) => currentGameButton = gameButton;

        public void OpenGame()
        {
            uIManager.ShowScoringPage();
            scoreDisplay.SetTitle("GAME #" + currentGameData.Number.ToString());
            scoreDisplay.DisplayGamePreview();
            badmintonCourt.ToggleBadmintonCourt(true);
            badmintonCourt.SetupPlayersOnCourt(currentGameData);
            badmintonCourt.ToggleAllPlayerColliders(true);
            badmintonCourt.ToggleServiceSelectionPrompt(true);
            badmintonCourt.ToggleServiceIndicatorVisible(false);

            if (currentGameData.Rounds != null)
                rounds = currentGameData.Rounds;
        }

        public void StartGameButton()
        {
            scoreDisplay.DisplayGameScoring();
            scoreDisplay.UpdateScoreDisplay(currentGameData.ScoreA, currentGameData.ScoreB);
            badmintonCourt.ToggleAllPlayerColliders(false);
            SaveRound();
        }

        public void AddPointButton(bool teamA)
        {
            if (badmintonCourt.PlayersMoving())
                return;

            badmintonCourt.ToggleServiceIndicatorVisible(false);

            if (teamA)
                currentGameData.ScoreA = currentGameData.ScoreA + 1;
            else
                currentGameData.ScoreB = currentGameData.ScoreB + 1;

            scoreDisplay.UpdateScoreDisplay(currentGameData.ScoreA, currentGameData.ScoreB);

            SaveRound();            
            badmintonCourt.UpdateServerAfterPoint(currentGameData, rounds[rounds.Count - 2]);
        }

        public void UndoButton()
        {
            if (rounds.Count == 1)
            {
                Debug.Log("Can't go back any more");
                return;
            }

            if (badmintonCourt.PlayersMoving()) return;

            badmintonCourt.ToggleServiceIndicatorVisible(false);

            // Remove Last Round
            rounds.RemoveAt(rounds.Count - 1);

            // Update Game Data with the new last round
            currentGameData.Rounds = rounds;
            currentGameData.ScoreA = rounds[rounds.Count - 1].ScoreA;
            currentGameData.ScoreB = rounds[rounds.Count - 1].ScoreB;

            // Update score display
            scoreDisplay.UpdateScoreDisplay(currentGameData.ScoreA, currentGameData.ScoreB);

            // Check if there's only one round left
            if (rounds.Count == 1)
            {
                // Use the only remaining round to update the server
                badmintonCourt.UpdateServerAfterPoint(currentGameData, rounds[0]);
            }
            else
            {
                // Use the previous round to update the server
                badmintonCourt.UpdateServerAfterPoint(currentGameData, rounds[rounds.Count - 2]);
            }
        }

        public void CompleteGame()
        {
            currentGameButton.ToggleGamePlayed(true);
            gameButtonsDisplay.UpdateContentHeight();
            CloseGame();
        }

        public void CloseGame()
        {
            uIManager.ShowGamesPage();
            badmintonCourt.ToggleBadmintonCourt(false);
        }

        private void SaveRound()
        {
            Round round = new Round
            {
                ScoreA = currentGameData.ScoreA,
                ScoreB = currentGameData.ScoreB
            };

            foreach (KeyValuePair<Player, Player> player in currentGameData.TeamA)
            { round.Player2Position = player.Key.PositionOnCourt; round.Player1Position = player.Value.PositionOnCourt; }

            foreach (KeyValuePair<Player, Player> player in currentGameData.TeamB)
            { round.Player3Position = player.Key.PositionOnCourt; round.Player4Position = player.Value.PositionOnCourt; }

            rounds.Add(round);
            currentGameData.Rounds = rounds;

            //PrintRound();
        }








        private void PrintRound()
        {
            Debug.Log("Round Number: " + rounds.Count + " --- Score: " + rounds[rounds.Count - 1].ScoreA + " | " + rounds[rounds.Count - 1].ScoreB);
            Debug.Log("Player1 Pos: " + rounds[rounds.Count - 1].Player1Position);
            Debug.Log("Player2 Pos: " + rounds[rounds.Count - 1].Player2Position);
            Debug.Log("Player3 Pos: " + rounds[rounds.Count - 1].Player3Position);
            Debug.Log("Player4 Pos: " + rounds[rounds.Count - 1].Player4Position);
            Debug.Log("--------------------------------------------------------");
        }


        private void ValidateRequiredVariables()
        {
            if (uIManager == null) { Debug.LogError("Null References: " + uIManager.name); }
            if (badmintonCourt == null) { Debug.LogError("Null References: " + badmintonCourt.name); }
            if (scoreDisplay == null) { Debug.LogError("Null References: " + scoreDisplay.name); }
            if (gameButtonsDisplay == null) { Debug.LogError("Null References: " + gameButtonsDisplay.name); }
        }
    }
}
