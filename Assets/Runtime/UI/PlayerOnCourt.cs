using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Runtime.Data;

namespace Runtime.UI
{
    public class PlayerOnCourt : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private Image playerImage;
        [SerializeField] private Collider playerCollider;
        [SerializeField] private Canvas canvas;

        private Player playerData;

        private void Awake() => ValidateRequiredVariables();

        private void OnEnable()
        {
            // Hacky Fix to make sure player is visible
            canvas.overrideSorting = true;
            canvas.overrideSorting = false;
        }

        public Player Data() { return playerData; }

        public void SetPlayerData(Player player)
        {
            playerData = player;
            playerName.text = player.Name;
            playerImage.sprite = player.BodySprite;
        }

        public void ToggleCollider(bool active)
        {
            playerCollider.enabled = active;
        }

        private void ValidateRequiredVariables()
        {
            if (playerName == null) { Debug.LogError("Null References: " + playerName.name); }
            if (playerImage == null) { Debug.LogError("Null References: " + playerImage.name); }
            if (playerCollider == null) { Debug.LogError("Null References: " + playerCollider.name); }
            if (canvas == null) { Debug.LogError("Null References: " + canvas.name); }
        }
    }
}


