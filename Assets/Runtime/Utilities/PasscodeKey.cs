using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Utilities
{
    [RequireComponent(typeof(Button))]
    public class PasscodeKey : MonoBehaviour
    {
        [SerializeField] private string value;

        private PasscodeWidget passcodeWidget;

        public void Regiester(PasscodeWidget widget)
        {
            passcodeWidget = widget;

            GetComponent<Button>().onClick.AddListener(KeyPressed);
            GetComponentInChildren<TextMeshProUGUI>().text = value;
        }

        private void KeyPressed()
        {
            passcodeWidget.TryAppendInput(value);
        }
    }
}