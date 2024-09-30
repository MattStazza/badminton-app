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
        [SerializeField] private GameObject badmintonCourt;
        [SerializeField] private GameObject serviceIndicator;
        [Space]
        [SerializeField] private GameObject selectServerMessage;
        [SerializeField] private Button startButton;
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
        private bool playersMoving;

        private void Awake() => ValidateRequiredVariables();

        private void Start()
        {
            CachePlayerInitialPositions();
            ToggleBadmintonCourt(false);
            ToggleAllPlayerColliders(false);
            ToggleServiceSelectionPrompt(false);
            ToggleServiceIndicatorVisible(false);
        }

        public void ToggleBadmintonCourt(bool visible) => badmintonCourt.SetActive(visible);

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

        public void ToggleAllPlayerColliders(bool active)
        {
            player1.ToggleCollider(active);
            player2.ToggleCollider(active);
            player3.ToggleCollider(active);
            player4.ToggleCollider(active);
        }

        public void ToggleServiceSelectionPrompt(bool active)
        {
            selectServerMessage.SetActive(active);
            startButton.interactable = !active;
        }

        public void SetPlayerAsServer(PlayerOnCourt player)
        {
            if (playersMoving) return;

            ToggleServiceSelectionPrompt(false);

            // Team A
            if (player == player1 || player == player2)
            {
                AssignService(player);

                if (player.PlayerData().PositionOnCourt == PlayerPosition.Right)
                { MoveServiceIndicator(); ToggleServiceIndicatorVisible(true); return; }
                else
                    SwapPositions(true);
            }

            // Team B
            if (player == player3 || player == player4)
            {
                AssignService(player);

                if (player.PlayerData().PositionOnCourt == PlayerPosition.Right)
                { MoveServiceIndicator(); ToggleServiceIndicatorVisible(true); return; }
                else
                    SwapPositions(false);
            }
        }

        public void SwapPositions(bool teamA)
        {
            PlayerOnCourt playerToSwap1 = null;
            PlayerOnCourt playerToSwap2 = null;

            if (teamA)
            { playerToSwap1 = player1; playerToSwap2 = player2; }
            else
            { playerToSwap1 = player3; playerToSwap2 = player4; }

            PlayerPosition player1CurrentPosition = playerToSwap1.PlayerData().PositionOnCourt;
            PlayerPosition player2CurrentPosition = playerToSwap2.PlayerData().PositionOnCourt;
            playerToSwap1.PlayerData().PositionOnCourt = player2CurrentPosition;
            playerToSwap2.PlayerData().PositionOnCourt = player1CurrentPosition;
            Vector3 player1Position = playerToSwap1.transform.position;
            Vector3 player2Position = playerToSwap2.transform.position;

            MovePlayers(playerToSwap1, player2Position, playerToSwap2, player1Position);
        }

        public void ToggleServiceIndicatorVisible(bool visible) => serviceIndicator.SetActive(visible);

        public void MoveServiceIndicator() => serviceIndicator.transform.position = new Vector3(servingPlayer.transform.position.x, 1f, servingPlayer.transform.position.z);

        public void AssignService(PlayerOnCourt player) => servingPlayer = player;

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

        private void CachePlayerInitialPositions()
        {
            player1InitialPos = player1.transform.position;
            player2InitialPos = player2.transform.position;
            player3InitialPos = player3.transform.position;
            player4InitialPos = player4.transform.position;
        }


        private void ValidateRequiredVariables()
        {
            if (badmintonCourt == null) { Debug.LogError("Null References: " + badmintonCourt.name); }
            if (serviceIndicator == null) { Debug.LogError("Null References: " + serviceIndicator.name); }
            if (selectServerMessage == null) { Debug.LogError("Null References: " + selectServerMessage.name); }
            if (startButton == null) { Debug.LogError("Null References: " + startButton.gameObject.name); }
            if (player1 == null) { Debug.LogError("Null References: " + player1.name); }
            if (player2 == null) { Debug.LogError("Null References: " + player2.name); }
            if (player3 == null) { Debug.LogError("Null References: " + player3.name); }
            if (player4 == null) { Debug.LogError("Null References: " + player4.name); }
        }
    }
}