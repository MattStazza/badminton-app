using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Runtime.Utilities
{
    public class PasscodeWidget : MonoBehaviour
    {
        [SerializeField] private int maxCodeLength = 6;
        [SerializeField] private string clearValue = "X";
        [SerializeField] private string backValue = "<";
        [Space]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private List<PasscodeKey> keys = new List<PasscodeKey>();

        private string currentInput;

        private void Awake() => RegisterKeys();

        private void OnEnable() => inputField.text = "";

        private void RegisterKeys()
        {
            foreach (PasscodeKey key in keys)
            {
                key.Regiester(this);
            }
        }

        public void TryAppendInput(string value)
        {
            currentInput = inputField.text;

            if (value == clearValue)
            {
                inputField.text = "";
            }
            else if (value == backValue)
            {
                if (!string.IsNullOrEmpty(currentInput))
                    inputField.text = currentInput.Substring(0, currentInput.Length - 1);
            }
            else
            {
                if (currentInput.Length == maxCodeLength)
                    return;

                inputField.text = currentInput + value;
            }
        }
    }
}