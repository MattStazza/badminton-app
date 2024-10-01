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

        private Player playerData;

        private void Awake() => ValidateRequiredVariables();

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
        }
    }
}


