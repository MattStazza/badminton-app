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
        //private List<Round> rounds = new List<Round>();
        //private bool teamAScoredLast = false;
        //private bool teamBScoredLast = false;


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

            // Pull exisiting game rounds (if they exist)
            //rounds = currentGameData.Rounds;
        }
        public void StartGame()
        {
            scoreDisplay.DisplayGameScoring();
            scoreDisplay.UpdateScoreDisplay(currentGameData.ScoreA, currentGameData.ScoreB);
            badmintonCourt.ToggleAllPlayerColliders(false);

            //SaveRound(currentGameData);
        }

        public void AddPoint(bool teamA)
        {
            if (teamA)
            {
                currentGameData.ScoreA = currentGameData.ScoreA + 1;
            }
            else
            {
                currentGameData.ScoreB = currentGameData.ScoreB + 1;
            }
                

            scoreDisplay.UpdateScoreDisplay(currentGameData.ScoreA, currentGameData.ScoreB);
            
            
            //SaveRound(currentGameData);
            //badmintonCourt.MovePlayers(currentGameRounds[currentGameRounds.Count]);
        }

        private void UpdatePlayerPositionData()
        {
            //foreach (KeyValuePair<Player, Player> player in game.TeamA)
            //{ player2Data = player.Key; player1Data = player.Value; }


            //foreach (KeyValuePair<Player, Player> player in game.TeamB)
            //{ player3Data = player.Key; player4Data = player.Value; }
        }

/*        private void SaveRound(Game gameData)
        {
            Round round = null;

            round.ScoreA = gameData.ScoreA;
            round.ScoreB = gameData.ScoreB;

            // Update Player Positions!

            rounds.Add(round);
            gameData.Rounds = rounds;
        }*/

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




        private void ValidateRequiredVariables()
        {
            if (uIManager == null) { Debug.LogError("Null References: " + uIManager.name); }
            if (badmintonCourt == null) { Debug.LogError("Null References: " + badmintonCourt.name); }
            if (scoreDisplay == null) { Debug.LogError("Null References: " + scoreDisplay.name); }
            if (gameButtonsDisplay == null) { Debug.LogError("Null References: " + gameButtonsDisplay.name); }
        }
    }
}
