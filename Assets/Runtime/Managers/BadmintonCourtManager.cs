using UnityEngine;
using Runtime.UI;
using Runtime.Data;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

namespace Runtime.Managers 
{
    public class BadmintonCourtManager : MonoBehaviour
    {
        [SerializeField] private GameSetupController gamePage;
        [SerializeField] private GameObject badmintonCourt;
        [SerializeField] private GameObject serviceIndicator;
        [Space]
        [Header("Team A")]
        [SerializeField] private PlayerOnCourt player1;
        [SerializeField] private PlayerOnCourt player2;
        [Header("Team B")]
        [SerializeField] private PlayerOnCourt player3;
        [SerializeField] private PlayerOnCourt player4;

        private PlayerOnCourt servingPlayer;
        private Vector3 player1InitialPos;
        private Vector3 player2InitialPos;
        private Vector3 player3InitialPos;
        private Vector3 player4InitialPos;
        private bool teamAServedLast;
        private bool teamAServedFirst;
        private bool playersMoving;


        private void Awake() => ValidateRequiredVariables();

        private void Start()
        {
            CachePlayerInitialPositions();
            ToggleBadmintonCourt(false);
            ToggleAllPlayerColliders(false);
            ToggleServiceIndicatorVisible(false);
        }

        public bool PlayersMoving() { return playersMoving; }
        public void ToggleBadmintonCourt(bool visible) => badmintonCourt.SetActive(visible);
        public void ToggleServiceIndicatorVisible(bool visible) => serviceIndicator.SetActive(visible);
        public void ToggleAllPlayerColliders(bool active)
        {
            player1.ToggleCollider(active);
            player2.ToggleCollider(active);
            player3.ToggleCollider(active);
            player4.ToggleCollider(active);
        }

        public void SetupPlayersOnCourt(Game game)
        {
            Player player1Data = null;
            Player player2Data = null;
            Player player3Data = null;
            Player player4Data = null;

            foreach (KeyValuePair<Player, Player> player in game.TeamA)
            {
                player2Data = player.Key;
                player.Value.PositionOnCourt = PlayerPosition.Left;
                player2.transform.position = player2InitialPos;

                player1Data = player.Value;
                player.Key.PositionOnCourt = PlayerPosition.Right;
                player1.transform.position = player1InitialPos;
            }

            foreach (KeyValuePair<Player, Player> player in game.TeamB)
            {
                player3Data = player.Key;
                player.Key.PositionOnCourt = PlayerPosition.Left;
                player3.transform.position = player3InitialPos;

                player4Data = player.Value;
                player.Value.PositionOnCourt = PlayerPosition.Right;
                player4.transform.position = player4InitialPos;
            }

            player1.SetPlayerData(player1Data);
            player2.SetPlayerData(player2Data);
            player3.SetPlayerData(player3Data);
            player4.SetPlayerData(player4Data);
        }


        public void AssignInitialServer(PlayerOnCourt player)
        {
            if (playersMoving) return;

            gamePage.ToggleStartButtonInteractable(true);

            bool isTeamA = (player == player1 || player == player2);
            teamAServedLast = isTeamA;
            teamAServedFirst = isTeamA;
            AssignService(player);

            if (player.Data().PositionOnCourt == PlayerPosition.Right)
            {
                MoveServiceIndicator();
                ToggleServiceIndicatorVisible(true);
            }
            else
                SwapPositions(isTeamA);
        }


        public void UpdateServerAfterPoint(Game game, Round lastRound)
        {
            if (game.ScoreA > lastRound.ScoreA)
            {
                HandleServe(game.ScoreA, player1, player2, true, "Team A's Serve");
            }
            else if (game.ScoreB > lastRound.ScoreB)
            {
                HandleServe(game.ScoreB, player3, player4, false, "Team B's Serve");
            }

            // Check which team served first??
            if (game.ScoreA == 0 && game.ScoreB == 0)
            {
                if (teamAServedFirst)
                    HandleServe(game.ScoreA, player1, player2, true, "Team A's Serve");

                if (!teamAServedFirst)
                    HandleServe(game.ScoreB, player3, player4, false, "Team B's Serve");
            }
        }

        


        // PRIVATE FUNCTIONS
        private void AssignService(PlayerOnCourt player) => servingPlayer = player;

        private void HandleServe(int score, PlayerOnCourt rightPlayer, PlayerOnCourt leftPlayer, bool isTeamA, string logMessage)
        {
            if (teamAServedLast == isTeamA)
                SwapPositions(isTeamA);

            PlayerOnCourt server = IsEven(score)
                ? rightPlayer.Data().PositionOnCourt == PlayerPosition.Right ? rightPlayer : leftPlayer
                : rightPlayer.Data().PositionOnCourt == PlayerPosition.Left ? rightPlayer : leftPlayer;

            AssignService(server);

            teamAServedLast = isTeamA;
            MoveServiceIndicator();

            if (!playersMoving)
                ToggleServiceIndicatorVisible(true);
        }

        private void SwapPositions(bool teamA)
        {
            PlayerOnCourt playerToSwap1 = null;
            PlayerOnCourt playerToSwap2 = null;

            if (teamA)
            { playerToSwap1 = player1; playerToSwap2 = player2; }
            else
            { playerToSwap1 = player3; playerToSwap2 = player4; }

            PlayerPosition player1CurrentPosition = playerToSwap1.Data().PositionOnCourt;
            PlayerPosition player2CurrentPosition = playerToSwap2.Data().PositionOnCourt;
            playerToSwap1.Data().PositionOnCourt = player2CurrentPosition;
            playerToSwap2.Data().PositionOnCourt = player1CurrentPosition;
            Vector3 player1Position = playerToSwap1.transform.position;
            Vector3 player2Position = playerToSwap2.transform.position;

            MovePlayers(playerToSwap1, player2Position, playerToSwap2, player1Position);
        }

        private void MovePlayers(PlayerOnCourt player1, Vector3 player1Pos, PlayerOnCourt player2, Vector3 player2Pos)
        {
            playersMoving = true;
            StartCoroutine(MovePlayerOverTime(player1, player1Pos, 0.25f));
            StartCoroutine(MovePlayerOverTime(player2, player2Pos, 0.25f));
        }

        private IEnumerator MovePlayerOverTime(PlayerOnCourt player, Vector3 targetPos, float duration)
        {
            float elapsedTime = 0f;
            Vector3 startingPos = player.transform.position;

            while (elapsedTime < duration)
            {
                player.transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            player.transform.position = targetPos;

            MoveServiceIndicator();
            ToggleServiceIndicatorVisible(true);
            playersMoving = false;
        }

        private void MoveServiceIndicator() => serviceIndicator.transform.position = new Vector3(servingPlayer.transform.position.x, (servingPlayer.transform.position.y - 1), servingPlayer.transform.position.z);

        private void CachePlayerInitialPositions()
        {
            player1InitialPos = player1.transform.position;
            player2InitialPos = player2.transform.position;
            player3InitialPos = player3.transform.position;
            player4InitialPos = player4.transform.position;
        }

        private bool IsEven(int number) { return number % 2 == 0; }

        private void ValidateRequiredVariables()
        {
            if (badmintonCourt == null) { Debug.LogError("Null References: " + badmintonCourt.name); }
            if (serviceIndicator == null) { Debug.LogError("Null References: " + serviceIndicator.name); }
            if (gamePage == null) { Debug.LogError("Null References: " + gamePage.name); }
            if (player1 == null) { Debug.LogError("Null References: " + player1.name); }
            if (player2 == null) { Debug.LogError("Null References: " + player2.name); }
            if (player3 == null) { Debug.LogError("Null References: " + player3.name); }
            if (player4 == null) { Debug.LogError("Null References: " + player4.name); }
        }
    }
}