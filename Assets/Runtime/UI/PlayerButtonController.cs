using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Runtime.UI
{
    public class PlayerButtonController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image profileImage;

        private void Awake() => ValidateRequiredVariables();

        public void SetupPlayer(string name, Sprite profile)
        {
            nameText.text = name;
            profileImage.sprite = profile;
        }

        public void ToggleButtonInteractive(bool interactive)
        {
            button.interactable = interactive;
        }


        private void ValidateRequiredVariables()
        {
            if (button == null) { Debug.LogError("Null References: " + button.name); }
            if (nameText == null) { Debug.LogError("Null References: " + nameText.name); }
            if (profileImage == null) { Debug.LogError("Null References: " + profileImage.name); }
        }
    }

}
