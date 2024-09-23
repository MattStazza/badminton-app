using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Runtime.Data;

namespace Runtime.UI
{
    public class PlayerButtonController : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image profileImage;
        [Space]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite selectedSprite;

        private PlayerData playerData;
        private bool selected;




        private void Awake() => ValidateRequiredVariables();



        public bool GetSelected() { return selected; }
        public PlayerData GetPlayerData() { return playerData; }



        public void SetupPlayerButton(PlayerData data)
        {
            playerData = data;
            nameText.text = data.playerName;
            profileImage.sprite = data.profileTexture;
        }

        public void ToggleButtonInteractive(bool interactive)
        {
            button.interactable = interactive;
        }

        public void ToggleSelected()
        {
            selected = !selected;

            if (selected)
                button.image.sprite = selectedSprite;
            else
                button.image.sprite = defaultSprite;
        }






        private void ValidateRequiredVariables()
        {
            if (button == null) { Debug.LogError("Null References: " + button.name); }
            if (nameText == null) { Debug.LogError("Null References: " + nameText.name); }
            if (profileImage == null) { Debug.LogError("Null References: " + profileImage.name); }
            if (defaultSprite == null) { Debug.LogError("Null References: " + defaultSprite.name); }
            if (selectedSprite == null) { Debug.LogError("Null References: " + selectedSprite.name); }
        }
    }

}
