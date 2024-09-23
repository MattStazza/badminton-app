using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;
using Runtime.Managers;


namespace Runtime.UI
{
    public class GeneratePlayerButtons : MonoBehaviour
    {
        [SerializeField] private SetupManager setupManager;
        [Space]
        [SerializeField] private Players players;
        [SerializeField] private GameObject playerButtonPrefab;

        private List<PlayerButtonController> playerButtons = new List<PlayerButtonController>();


        private void Awake() => ValidateRequiredVariables();
        private void Start() => GenerateButtons();


        private void GenerateButtons()
        {
            foreach (PlayerData playerData in players.GetAllPlayerData())
            {
                // Spawn Button for every Player
                GameObject playerButton = Instantiate(playerButtonPrefab, transform);
                PlayerButtonController playerButtonController = playerButton.GetComponent<PlayerButtonController>();
                playerButtons.Add(playerButtonController);

                // Setup Button Display & Toggle Active
                playerButtonController.SetupPlayerButton(playerData);
                playerButtonController.ToggleButtonInteractive(true);
            }
        }

        public void AddSelectedPlayers()
        {
            setupManager.ClearSelectedPlayers();

            foreach (PlayerButtonController button in playerButtons)
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


