using UnityEngine;
using Runtime.Data;

namespace Runtime.UI.Results
{
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

        private void Awake() => ValidateRequiredVariables();

        public void SetupPodium()
        {
            TopThreeRanking ranking = Session.DetermineTopThreeRanking();

            switch (ranking)
            {
                case TopThreeRanking.FirstFirstFirst:
                    SetupStands(StandLevel.First, StandLevel.First, StandLevel.First);
                    break;

                case TopThreeRanking.FirstFirstSecond:
                    SetupStands(StandLevel.First, StandLevel.First, StandLevel.Second);
                    break;

                case TopThreeRanking.FirstSecondSecond:
                    SetupStands(StandLevel.First, StandLevel.Second, StandLevel.Second);
                    break;

                case TopThreeRanking.FirstSecondThird:
                    SetupStands(StandLevel.First, StandLevel.Second, StandLevel.Third);
                    break;

                default:
                    Debug.LogWarning("Unsupported TopThreeRanking: " + ranking);
                    break;
            }
        }

        private void SetupStands(StandLevel standOne, StandLevel standTwo, StandLevel standThree)
        {
            middlePodiumStand.SetupStand(standOne, Session.GetTopThreePlayers()[0]);
            leftPodiumStand.SetupStand(standTwo, Session.GetTopThreePlayers()[1]);
            rightPodiumStand.SetupStand(standThree, Session.GetTopThreePlayers()[2]);
        }

        private void ValidateRequiredVariables()
        {
            if (middlePodiumStand == null) { Debug.LogError("Null References: " + middlePodiumStand.name); }
            if (leftPodiumStand == null) { Debug.LogError("Null References: " + leftPodiumStand.name); }
            if (rightPodiumStand == null) { Debug.LogError("Null References: " + rightPodiumStand.name); }
        }
    }
}