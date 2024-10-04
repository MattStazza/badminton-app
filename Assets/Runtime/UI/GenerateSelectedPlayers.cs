using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;
using Runtime.Managers;
using Runtime.UI;


namespace Runtime.UI
{
    public class GenerateSelectedPlayers : MonoBehaviour
    {
        [SerializeField] private SetupManager setupManager;
        [SerializeField] private GameObject playerButtonPrefab;
        [SerializeField] private GameObject addPlayerButton;

        private List<PlayerButton> selectedPlayers = new List<PlayerButton>();


        private void Awake() => ValidateRequiredVariables();

        private void OnEnable() => RefreshSelectedPlayers();

        private void RefreshSelectedPlayers()
        {
            foreach (PlayerButton playerButton in selectedPlayers)
                Destroy(playerButton.gameObject);

            selectedPlayers.Clear();

            ShowSelectedPlayers();

            addPlayerButton.transform.SetAsLastSibling();
        }

        private void ShowSelectedPlayers()
        {
            if (setupManager.GetSelectedPlayers().Count == 0)
                return;

            foreach (PlayerData playerData in setupManager.GetSelectedPlayers())
            {
                // Spawn Selected Players
                GameObject player = Instantiate(playerButtonPrefab, transform);
                PlayerButton playerButtonController = player.GetComponent<PlayerButton>();
                selectedPlayers.Add(playerButtonController);

                // Setup Players Display & Toggle Inactive
                playerButtonController.UpdatePlayerButtonDisplay(playerData);
                playerButtonController.ToggleButtonInteractive(false);
            }
        }

        private void ValidateRequiredVariables()
        {
            if (setupManager == null) { Debug.LogError("Null References: " + setupManager.name); }
            if (playerButtonPrefab == null) { Debug.LogError("Null References: " + playerButtonPrefab.name); }
            if (addPlayerButton == null) { Debug.LogError("Null References: " + addPlayerButton.name); }
        }
    }
}


