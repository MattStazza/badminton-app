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
        [Space]
        [SerializeField] private ScoreDisplayController scoringPage;
        [SerializeField] private PopupMessage popupMessage;
        [SerializeField] private Timer timer;

        private GameButton currentGameButton;
        private List<Round> rounds = new List<Round>();

        private void Awake() => ValidateRequiredVariables();

        public void SetCurrentGameButton(GameButton gameButton) => currentGameButton = gameButton;

        public void OpenGame()
        {
            if (Session.CurrentGame.Complete) { ShowGameResults(); return; }

            uIManager.ShowGamePage();
            badmintonCourt.ToggleBadmintonCourt(true);
            badmintonCourt.SetupPlayersOnCourt(Session.CurrentGame);
            badmintonCourt.ToggleAllPlayerColliders(true);
            badmintonCourt.ToggleServiceIndicatorVisible(false);

            rounds = new List<Round>();
        }

        public void BackButton()
        {
            uIManager.ShowAllGamesPage();
            badmintonCourt.ToggleBadmintonCourt(false);
            
        }

        public void SwapButton()
        {
            List<Player> team = Session.CurrentGame.TeamA; // Cache TeamA

            Session.CurrentGame.TeamA = Session.CurrentGame.TeamB;
            Session.CurrentGame.TeamB = team;

            badmintonCourt.SetupPlayersOnCourt(Session.CurrentGame);
        }

        public void StartGameButton()
        {
            uIManager.ShowScoringPage();
            timer.ResetTimer();
            scoringPage.UpdateScoreDisplay(Session.CurrentGame.ScoreA, Session.CurrentGame.ScoreB);
            badmintonCourt.ToggleAllPlayerColliders(false);
            SaveRound();
        }

        public void AddPointButton(bool teamA)
        {
            if (badmintonCourt.PlayersMoving())
                return;

            badmintonCourt.ToggleServiceIndicatorVisible(false);

            if (teamA)
                Session.CurrentGame.ScoreA = Session.CurrentGame.ScoreA + 1;
            else
                Session.CurrentGame.ScoreB = Session.CurrentGame.ScoreB + 1;

            scoringPage.UpdateScoreDisplay(Session.CurrentGame.ScoreA, Session.CurrentGame.ScoreB);

            CheckGameProgress();

            SaveRound();            
            badmintonCourt.UpdateServerAfterPoint(Session.CurrentGame, rounds[rounds.Count - 2]);
        }

        public void UndoButton()
        {
            if (rounds.Count == 1)
            {
                uIManager.ShowGamePage();
                badmintonCourt.ToggleServiceIndicatorVisible(false);
                badmintonCourt.ToggleAllPlayerColliders(true);

                rounds.Clear();
                Session.CurrentGame.Rounds = rounds;

                return;
            }

            if (badmintonCourt.PlayersMoving()) return;

            badmintonCourt.ToggleServiceIndicatorVisible(false);

            rounds.RemoveAt(rounds.Count - 1);

            Session.CurrentGame.Rounds = rounds;
            Session.CurrentGame.ScoreA = rounds[rounds.Count - 1].ScoreA;
            Session.CurrentGame.ScoreB = rounds[rounds.Count - 1].ScoreB;

            scoringPage.UpdateScoreDisplay(Session.CurrentGame.ScoreA, Session.CurrentGame.ScoreB);

            if (rounds.Count == 1)
                badmintonCourt.UpdateServerAfterPoint(Session.CurrentGame, rounds[0]);
            else
                badmintonCourt.UpdateServerAfterPoint(Session.CurrentGame, rounds[rounds.Count - 2]);
        }


        public void ShowGameResults()
        {
            if (!Session.CurrentGame.Complete)
            {
                Session.CurrentGame.Duration = timer.GetFormatTime();
                timer.StopTimer();
            }

            uIManager.ShowGameResultsPage();
            badmintonCourt.ToggleBadmintonCourt(false);
        }

        public void CompleteGame()
        {
            if (Session.CurrentGame.Complete) return;

            SavePlayerStats();
            Session.CurrentGame.Complete = true;
            Session.CurrentGame.Duration = timer.GetFormatTime();
            currentGameButton.ToggleGamePlayed(Session.CurrentGame.Complete);
        }



        private void SavePlayerStats()
        {
            var currentGame = Session.CurrentGame;

            // Update Games Played
            foreach (Player player in currentGame.TeamA)
                player.GamesPlayed++;

            foreach (Player player in currentGame.TeamB)
                player.GamesPlayed++;

            // Determine winning and losing teams
            var winningTeam = currentGame.ScoreA > currentGame.ScoreB ? currentGame.TeamA : currentGame.TeamB;
            var losingTeam = currentGame.ScoreA > currentGame.ScoreB ? currentGame.TeamB : currentGame.TeamA;

            // Check for a deuce win
            bool isDeuceWin = Mathf.Max(currentGame.ScoreA, currentGame.ScoreB) > 21;

            // Update stats for the winning team
            if (isDeuceWin)
                foreach (Player player in winningTeam)
                    player.DeuceWins++;
            else
                foreach (Player player in winningTeam)
                    player.Wins++;

            // Update stats for the losing team
            if (isDeuceWin)
                foreach (Player player in losingTeam)
                    player.DeuceLosses++;
            else
                foreach (Player player in losingTeam)
                    player.Losses++;
        }

        private void CheckGameProgress()
        {
            if (Session.CurrentGame.ScoreA == 20 || Session.CurrentGame.ScoreB == 20)
                popupMessage.DisplayPopupMessage("MATCH POINT!");

            if ((Session.CurrentGame.ScoreA >= 20 || Session.CurrentGame.ScoreB >= 20) && Session.CurrentGame.ScoreB == Session.CurrentGame.ScoreA)
                popupMessage.DisplayPopupMessage("DEUCE!");

            if (Session.CurrentGame.ScoreA >= 21 || Session.CurrentGame.ScoreB >= 21)
            {
                // Win!
                if (Session.CurrentGame.ScoreA >= Session.CurrentGame.ScoreB + 2)
                { ShowGameResults(); return; } // Team A Wins

                if (Session.CurrentGame.ScoreB >= Session.CurrentGame.ScoreA + 2)
                { ShowGameResults(); return; }  // Team B Wins

                // Advantage
                if (Session.CurrentGame.ScoreA >= Session.CurrentGame.ScoreB + 1)
                    popupMessage.DisplayPopupMessage("ADVANTAGE TEAM A");

                if (Session.CurrentGame.ScoreB >= Session.CurrentGame.ScoreA + 1)
                    popupMessage.DisplayPopupMessage("ADVANTAGE TEAM B");
            }
        }

        private void SaveRound()
        {
            Round round = new Round
            {
                ScoreA = Session.CurrentGame.ScoreA,
                ScoreB = Session.CurrentGame.ScoreB
            };

            round.Player1Position = Session.CurrentGame.TeamA[1].PositionOnCourt;
            round.Player2Position = Session.CurrentGame.TeamA[0].PositionOnCourt;

            round.Player3Position = Session.CurrentGame.TeamB[0].PositionOnCourt;
            round.Player4Position = Session.CurrentGame.TeamB[1].PositionOnCourt;

            rounds.Add(round);
            Session.CurrentGame.Rounds = rounds;
        }


        private void ValidateRequiredVariables()
        {
            if (uIManager == null) { Debug.LogError("Null References: " + uIManager.name); }
            if (badmintonCourt == null) { Debug.LogError("Null References: " + badmintonCourt.name); }
            if (scoringPage == null) { Debug.LogError("Null References: " + scoringPage.name); }
            if (timer == null) { Debug.LogError("Null References: " + timer.name); }
            if (popupMessage == null) { Debug.LogError("Null References: " + popupMessage.name); }
        }
    }
}
