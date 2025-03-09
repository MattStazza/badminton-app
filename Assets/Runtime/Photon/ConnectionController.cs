using UnityEngine;
using Photon.Pun;
using Runtime.Managers;
using Photon.Realtime;
using Runtime.UI;

namespace Runtime.Photon
{
    public class ConnectionController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UIManager uIManager;
        [SerializeField] private PopupMessage popupMessage;

        private void Awake() => ValidateRequiredVariables();


        // Connecting / Joining
        public void ConnectToPhoton() => PhotonNetwork.ConnectUsingSettings();

        public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

        public override void OnJoinedLobby()
        {
            popupMessage.DisplayPopupMessage("CONNECTED");
            uIManager.ShowConnectedPage();
        }


        // Disconnecting / Leaving
        public void DisconnectFromPhoton() => PhotonNetwork.LeaveLobby();

        public override void OnLeftLobby() => PhotonNetwork.Disconnect();

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            popupMessage.DisplayPopupMessage("DISCONNECTED");
            uIManager.ShowHomePage();
        }


        private void ValidateRequiredVariables()
        {
            if (uIManager == null) { Debug.LogError("Null References: " + uIManager.name); }
            if (popupMessage == null) { Debug.LogError("Null References: " + popupMessage.name); }
        }
    }
}