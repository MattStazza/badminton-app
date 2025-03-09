using UnityEngine;
using Photon.Pun;
using Runtime.Managers;
using Runtime.UI;
using TMPro;

namespace Runtime.Photon
{
    public class RoomsManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject networkedPlayer;
        [SerializeField] private TMP_InputField hostInputField;
        [SerializeField] private TMP_InputField joinInputField;
        [Space]
        [SerializeField] private UIManager uiManager;
        [SerializeField] private PopupMessage popupMessage;

        private void Awake() => ValidateRequiredVariables();

        public void CreateRoom() => PhotonNetwork.CreateRoom(hostInputField.text);
        public void JoinRoom() => PhotonNetwork.JoinRoom(joinInputField.text);

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            uiManager.ShowLobbyPage();
            PhotonNetwork.Instantiate(networkedPlayer.name, transform.position, Quaternion.identity);
            popupMessage.DisplayPopupMessage("JOINED ROOM");
        }
        private void ValidateRequiredVariables()
        {
            if (networkedPlayer == null) { Debug.LogError("Null References: " + networkedPlayer.name); }
            if (hostInputField == null) { Debug.LogError("Null References: " + hostInputField.name); }
            if (joinInputField == null) { Debug.LogError("Null References: " + joinInputField.name); }
            if (uiManager == null) { Debug.LogError("Null References: " + uiManager.name); }
            if (popupMessage == null) { Debug.LogError("Null References: " + popupMessage.name); }
        }
    }
}