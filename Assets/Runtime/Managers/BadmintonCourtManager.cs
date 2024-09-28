using UnityEngine;
using Runtime.UI;
using Runtime.Data;
using System.Collections.Generic;

namespace Runtime.Managers 
{
    public class BadmintonCourtManager : MonoBehaviour
    {
        [SerializeField] private GameObject badmintonCourt;
        [Space]
        [Header("Team A")]
        [SerializeField] PlayerOnCourtButtonController player1;
        [SerializeField] PlayerOnCourtButtonController player2;
        [Space]
        [Header("Team B")]
        [SerializeField] PlayerOnCourtButtonController player3;
        [SerializeField] PlayerOnCourtButtonController player4;

        private void Awake() => ValidateRequiredVariables();
        private void Start() => ToggleBadmintonCourt(false);

        public void ToggleBadmintonCourt(bool visible) => badmintonCourt.SetActive(visible);

        public void SetupPlayersOnCourt(Game game)
        {
            Player player1Data = null;
            Player player2Data = null;
            Player player3Data = null;
            Player player4Data = null;

            foreach (KeyValuePair<Player, Player> player in game.TeamA)
            { player2Data = player.Key; player1Data = player.Value; }

            foreach (KeyValuePair<Player, Player> player in game.TeamB)
            { player3Data = player.Key; player4Data = player.Value; }

            player1.SetupPlayer(player1Data);
            player2.SetupPlayer(player2Data);
            player3.SetupPlayer(player3Data);
            player4.SetupPlayer(player4Data);
        }

        private void ValidateRequiredVariables()
        {
            if (badmintonCourt == null) { Debug.LogError("Null References: " + badmintonCourt.name); }
            if (player1 == null) { Debug.LogError("Null References: " + player1.name); }
            if (player2 == null) { Debug.LogError("Null References: " + player2.name); }
            if (player3 == null) { Debug.LogError("Null References: " + player3.name); }
            if (player4 == null) { Debug.LogError("Null References: " + player4.name); }
        }
    }
}