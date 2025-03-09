using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Runtime.Utilities
{
    public class PasscodeWidget : MonoBehaviour
    {
        [Header("SETTINGS")]
        [SerializeField] private int maxCodeLength = 6;
        [SerializeField] private string clearValue = "X";
        [SerializeField] private string backValue = "<";
        [Space]
        [Header("REFERENCES")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button submitButton;
        [SerializeField] private List<PasscodeKey> keys = new List<PasscodeKey>();

        private string currentInput;

        private void Awake() => RegisterKeys();

        private void OnEnable() 
        {
            inputField.text = ""; 
            submitButton.interactable = false; 
        }

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
                submitButton.interactable = false;
            }
            else if (value == backValue)
            {
                if (!string.IsNullOrEmpty(currentInput))
                    inputField.text = currentInput.Substring(0, currentInput.Length - 1);

                if (string.IsNullOrEmpty(inputField.text))
                    submitButton.interactable = false;
            }
            else
            {
                if (currentInput.Length == maxCodeLength)
                    return;

                inputField.text = currentInput + value;
                submitButton.interactable = true;
            }
        }
    }
}