using UnityEngine;
using Runtime.Data;
using TMPro;

namespace Runtime.UI.Results
{

    public class SortScoreboardController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private PodiumController podium;
        [SerializeField] private ScoreboardController scoreboard;


        private void Awake() => ValidateRequiredVariables();

        private void Start() => dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        private void OnDropdownValueChanged(int index)
        {
            string selectedOption = dropdown.options[index].text;

            switch (selectedOption)
            {
                case "WINS":
                    ConfigurationSettings.ScoringMethod = ScoringMethod.ScoreByWins;
                    break;
                case "POINTS":
                    ConfigurationSettings.ScoringMethod = ScoringMethod.ScoreByPoints;
                    break;
                default:
                    Debug.LogWarning("Unhandled dropdown option: " + selectedOption);
                    break;
            }

            scoreboard.SetupScoreboard();
            podium.SetupPodium();
        }

        private void ValidateRequiredVariables()
        {
            if (dropdown == null) { Debug.LogError("Null References: " + dropdown.name); }
            if (podium == null) { Debug.LogError("Null References: " + podium.name); }
            if (scoreboard == null) { Debug.LogError("Null References: " + scoreboard.name); }
        }
    }
}
