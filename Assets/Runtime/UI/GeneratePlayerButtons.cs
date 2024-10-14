using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;
using Runtime.Managers;
using UnityEngine.UI;

namespace Runtime.UI
{
    public class GeneratePlayerButtons : MonoBehaviour
    {
        [SerializeField] private SetupManager setupManager;
        [SerializeField] private Players players;
        [Space]
        [SerializeField] private GameObject playerButtonPrefab;

        private List<PlayerButton> playerButtons = new List<PlayerButton>();


        private void Awake() => ValidateRequiredVariables();
        private void Start() => GenerateButtons();


        private void GenerateButtons()
        {
            foreach (PlayerData playerData in players.GetAllPlayerData())
            {
                // Spawn Player Buttons
                GameObject playerButton = Instantiate(playerButtonPrefab, transform);
                PlayerButton playerButtonController = playerButton.GetComponent<PlayerButton>();
                playerButtons.Add(playerButtonController);

                // Setup Button Display & Toggle Active
                playerButtonController.UpdatePlayerButtonDisplay(playerData);
                playerButtonController.ToggleButtonInteractive(true);
            }
        }

        public void AddSelectedPlayers()
        {
            setupManager.ClearSelectedPlayers();

            foreach (PlayerButton button in playerButtons)
            {
                if (button.GetSelected())
                    setupManager.AddPlayerToSelected(button.GetPlayerData());
            }
        }


        private void ValidateRequiredVariables()
        {
            if (setupManager == null) { Debug.LogError("Null References: " + setupManager.name); }
            if (players == null) { Debug.LogError("Null References: " + players.name); }
            if (playerButtonPrefab == null) { Debug.LogError("Null References: " + playerButtonPrefab.name); }
        }
    }
}


