using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;
using System.Linq;

namespace Runtime.UI.Results
{
    public enum PodiumConfiguration
    {
        FirstFirstFirst,
        FirstFirstSecond,
        FirstSecondSecond,
        FirstSecondThird
    }
    public enum StandLevel
    {
        First,
        Second,
        Third
    }



    public class PodiumController : MonoBehaviour
    {
        [SerializeField] private StandController middlePodiumStand;
        [SerializeField] private StandController leftPodiumStand;
        [SerializeField] private StandController rightPodiumStand;

        private List<Player> allPlayers = new List<Player>();
        private List<Player> topTheePlayers = new List<Player>();


        private void Awake() => ValidateRequiredVariables();

        public void SetupPodium()
        {
            allPlayers = Session.Players;
            GetTopThreePlayers(allPlayers);

            PodiumConfiguration config = DeterminePodiumConfiguration();

            switch (config)
            {
                case PodiumConfiguration.FirstFirstFirst:
                    SetupStands(StandLevel.First, StandLevel.First, StandLevel.First);
                    break;

                case PodiumConfiguration.FirstFirstSecond:
                    SetupStands(StandLevel.First, StandLevel.First, StandLevel.Second);
                    break;

                case PodiumConfiguration.FirstSecondSecond:
                    SetupStands(StandLevel.First, StandLevel.Second, StandLevel.Second);
                    break;

                case PodiumConfiguration.FirstSecondThird:
                    SetupStands(StandLevel.First, StandLevel.Second, StandLevel.Third);
                    break;

                default:
                    Debug.LogWarning("Unsupported PodiumConfiguration: " + config);
                    break;
            }
        }


        private void SetupStands(StandLevel standOne, StandLevel standTwo, StandLevel standThree)
        {
            middlePodiumStand.SetupStand(standOne, topTheePlayers[0]);
            leftPodiumStand.SetupStand(standTwo, topTheePlayers[1]);
            rightPodiumStand.SetupStand(standThree, topTheePlayers[2]);
        }

        private void GetTopThreePlayers(List<Player> players)
        {
            topTheePlayers = allPlayers
                .OrderByDescending(player => player.Score())   
                .ThenBy(player => player.GamesPlayed)          
                .Take(3)                                       
                .ToList();
        }

        private PodiumConfiguration DeterminePodiumConfiguration()
        {
            // Ensure we have exactly 3 players
            if (topTheePlayers.Count < 3)
            {
                Debug.LogWarning("Not enough players to determine podium configuration.");
                return PodiumConfiguration.FirstSecondThird;
            }

            // Check for ties
            bool firstPlaceTie = topTheePlayers[0].Score() == topTheePlayers[1].Score();
            bool secondPlaceTie = topTheePlayers[1].Score() == topTheePlayers[2].Score();

            // FirstFirstSecond: A tie for first, and a clear third place
            if (firstPlaceTie && !secondPlaceTie)
                return PodiumConfiguration.FirstFirstSecond;

            // FirstSecondSecond: A clear first, and a tie for second
            if (!firstPlaceTie && secondPlaceTie)
                return PodiumConfiguration.FirstSecondSecond;

            // FirstFirstFirst: All three have the same score
            if (firstPlaceTie && secondPlaceTie)
                return PodiumConfiguration.FirstFirstFirst;

            // Default: First, Second, Third (no ties)
            return PodiumConfiguration.FirstSecondThird;
        }

        private void ValidateRequiredVariables()
        {
            if (middlePodiumStand == null) { Debug.LogError("Null References: " + middlePodiumStand.name); }
            if (leftPodiumStand == null) { Debug.LogError("Null References: " + leftPodiumStand.name); }
            if (rightPodiumStand == null) { Debug.LogError("Null References: " + rightPodiumStand.name); }
        }
    }
}