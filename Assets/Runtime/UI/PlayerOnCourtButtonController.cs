using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Runtime.Data;

namespace Runtime.UI
{
    public class PlayerOnCourtButtonController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private Image playerImage;

        private void Awake() => ValidateRequiredVariables();

        public void SetupPlayer(Player player)
        {
            playerName.text = player.Name;
            playerImage.sprite = player.BodySprite;
        }

        private void ValidateRequiredVariables()
        {
            if (playerName == null) { Debug.LogError("Null References: " + playerName.name); }
            if (playerImage == null) { Debug.LogError("Null References: " + playerImage.name); }
        }
    }
}


