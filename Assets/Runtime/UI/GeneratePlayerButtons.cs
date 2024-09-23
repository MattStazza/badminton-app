using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;


namespace Runtime.UI
{
    public class GeneratePlayerButtons : MonoBehaviour
    {
        [SerializeField] private Players players;
        [SerializeField] private GameObject playerButtonPrefab;

        private void Awake() => ValidateRequiredVariables();


        private void Start() => GenerateButtons();


        private void GenerateButtons()
        {
            foreach (PlayerData playerData in players.GetAllPlayers())
            {
                GameObject playerButton = Instantiate(playerButtonPrefab, transform);
                PlayerButtonController playerButtonController = playerButton.GetComponent<PlayerButtonController>();
                
                playerButtonController.SetupPlayer(playerData.playerName, playerData.profileTexture);
                playerButtonController.ToggleButtonInteractive(true);
            }
        }


        private void ValidateRequiredVariables()
        {
            if (players == null) { Debug.LogError("Null References: " + players.name); }
            if (playerButtonPrefab == null) { Debug.LogError("Null References: " + playerButtonPrefab.name); }
        }
    }
}


