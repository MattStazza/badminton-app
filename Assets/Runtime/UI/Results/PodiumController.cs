using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        [SerializeField] private PodiumConfiguration config;
        [Space]
        [SerializeField] private StandController middlePodiumStand;
        [SerializeField] private StandController leftPodiumStand;
        [SerializeField] private StandController rightPodiumStand;

        private void Awake() => ValidateRequiredVariables();

        public void SetupPodium() => ConfigurePodium(config);


        private void ConfigurePodium(PodiumConfiguration config)
        {
            switch (config)
            {
                case PodiumConfiguration.FirstFirstFirst:
                    SetStandLevels(StandLevel.First, StandLevel.First, StandLevel.First);
                    break;

                case PodiumConfiguration.FirstFirstSecond:
                    SetStandLevels(StandLevel.First, StandLevel.First, StandLevel.Second);
                    break;

                case PodiumConfiguration.FirstSecondSecond:
                    SetStandLevels(StandLevel.First, StandLevel.Second, StandLevel.Second);
                    break;

                case PodiumConfiguration.FirstSecondThird:
                    SetStandLevels(StandLevel.First, StandLevel.Second, StandLevel.Third);
                    break;

                default:
                    Debug.LogWarning("Unsupported PodiumConfiguration: " + config);
                    break;
            }
        }


        private void SetStandLevels(StandLevel standOne, StandLevel standTwo, StandLevel standThree)
        {
            middlePodiumStand.SetupStand(standOne);
            leftPodiumStand.SetupStand(standTwo);
            rightPodiumStand.SetupStand(standThree);
        }


        private void ValidateRequiredVariables()
        {
            if (middlePodiumStand == null) { Debug.LogError("Null References: " + middlePodiumStand.name); }
            if (leftPodiumStand == null) { Debug.LogError("Null References: " + leftPodiumStand.name); }
            if (rightPodiumStand == null) { Debug.LogError("Null References: " + rightPodiumStand.name); }
        }
    }
}