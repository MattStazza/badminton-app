using UnityEngine;

namespace Runtime.UI
{
    public class ConnectedPageController : MonoBehaviour
    {
        [SerializeField] private PlayerButton playerProfile;

        private void Awake() => ValidateRequiredVariables();

        private void OnEnable() => ShowConnectedPage();

        private void ShowConnectedPage()
        {
            DisplayProfile();
        }

        private void DisplayProfile()
        {
            playerProfile.ToggleButtonInteractive(false);
            //playerProfile.
        }

        private void ValidateRequiredVariables()
        {
            if (playerProfile == null) { Debug.LogError("Null References: " + playerProfile.name); }
        }
    }

}