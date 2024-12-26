using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class GameSetupController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI title;
        [Space]
        [SerializeField] private Button startButton;
        [SerializeField] private GameObject selectServerMessage;

        private void Awake() => ValidateRequiredVariables();

        private void OnEnable() => ToggleStartButtonInteractable(false);

        public void SetTitle(string text) => title.text = text;

        public void ToggleStartButtonInteractable(bool interactable)
        {
            startButton.interactable = interactable;
            selectServerMessage.SetActive(!interactable);
        }

        private void ValidateRequiredVariables()
        {
            if (title == null) { Debug.LogError("Null References: " + title.name); }
            if (startButton == null) { Debug.LogError("Null References: " + startButton.name); }
            if (selectServerMessage == null) { Debug.LogError("Null References: " + selectServerMessage.name); }
        }
    }
}