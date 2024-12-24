using UnityEngine;
using TMPro;
using Runtime.Utilities;

namespace Runtime.UI
{
    public class PopupMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private ObjectMover moveAnimation;

        private void Awake() => ValidateRequiredVariables();

        public void DisplayPopupMessage(string text)
        {
            message.text = text;
            moveAnimation.MoveObject();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DisplayPopupMessage("TEST");
            }
        }

        private void ValidateRequiredVariables()
        {
            if (message == null) {Debug.LogError("Null References: " + message.name);}
            if (moveAnimation == null) {Debug.LogError("Null References: " + moveAnimation.name);}
        }
    }
}