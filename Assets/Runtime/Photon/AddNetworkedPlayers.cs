using UnityEngine;
using Photon.Pun;
using Runtime.Managers;
using Runtime.UI;
using TMPro;


namespace Runtime.Photon
{
    public class AddNetworkedPlayers : MonoBehaviour
    {
        [SerializeField] private GameObject networkedPlayer;
        [SerializeField] private Transform spawnTransform;
        [SerializeField] private PopupMessage popupMessage;

        private void Awake() => ValidateRequiredVariables();

        public void AddPlayer()
        {
            GameObject spawnedPlayer = PhotonNetwork.Instantiate(networkedPlayer.name, spawnTransform.position, Quaternion.identity);
            spawnedPlayer.transform.SetParent(spawnTransform);

            popupMessage.DisplayPopupMessage("PLAYER JOINED");
        }

        private void ValidateRequiredVariables()
        {
            if (popupMessage == null) { Debug.LogError("Null References: " + popupMessage.name); }
            if (networkedPlayer == null) { Debug.LogError("Null References: " + networkedPlayer.name); }
            if (spawnTransform == null) { Debug.LogError("Null References: " + spawnTransform.name); }
        }
    }
}