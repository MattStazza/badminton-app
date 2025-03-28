using UnityEngine;
using TMPro;
using Runtime.Data;
using UnityEngine.UI;

namespace Runtime.UI.Results
{
    public class SlotController : MonoBehaviour
    {
        [SerializeField] private PlayerButton playerButton;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI gamesPlayed;
        [SerializeField] private Image background;

        private void Awake() => ValidateRequiredVariables();

        public void DisplayPlayerStats(Player player)
        {
            PlayerData playerData = new PlayerData
            {
                playerName = player.Name,
                profileTexture = player.ProfileSprite,
                bodyTexture = player.BodySprite
            };

            playerButton.UpdatePlayerButtonDisplay(playerData);
            playerButton.ToggleButtonInteractive(false);

            score.text = player.Score().ToString();
            gamesPlayed.text = player.GamesPlayed.ToString();

        }

        public int GetSlotScore()
        {
            if (int.TryParse(score.text, out int result))
            {
                return result; 
            }
            else
            {
                Debug.LogWarning("Text is not a valid integer: " + score.text);
                return -1;
            }
        }

        public int GetSlotGames()
        {
            if (int.TryParse(gamesPlayed.text, out int result))
            {
                return result;
            }
            else
            {
                Debug.LogWarning("Text is not a valid integer: " + gamesPlayed.text);
                return -1;
            }
        }

        public void SetBackgroundColor(Color color)
        {
            background.color = color;
        }

        private void ValidateRequiredVariables()
        {
            if (playerButton == null) { Debug.LogError("Null References: " + playerButton.name); }
            if (score == null) { Debug.LogError("Null References: " + score.name); }
            if (gamesPlayed == null) { Debug.LogError("Null References: " + gamesPlayed.name); }
            if (background == null) { Debug.LogError("Null References: " + background.name); }
        }
    }
}