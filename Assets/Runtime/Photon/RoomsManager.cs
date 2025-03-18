using UnityEngine;
using Photon.Pun;
using Runtime.Managers;
using Runtime.UI;
using TMPro;
using Runtime.Data;
using Player = Photon.Realtime.Player;

namespace Runtime.Photon
{
    public class RoomsManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject networkedPlayer;
        [Space]
        [SerializeField] private TMP_InputField hostInputField;
        [SerializeField] private TMP_InputField joinInputField;
        [Space]
        [SerializeField] private UIManager uiManager;
        [SerializeField] private PopupMessage popupMessage;

        private void Awake() => ValidateRequiredVariables();




        public void CreateRoom()
        {
            SetupPlayer();
            PhotonNetwork.CreateRoom(hostInputField.text);
        }

        public void JoinRoom()
        {
            SetupPlayer();
            PhotonNetwork.JoinRoom(joinInputField.text);
        }




        private void SetupPlayer()
        {
            User user = new User();

            ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable();
            props["PlayerName"] = user.UserName.ToString();
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        }





        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            uiManager.ShowLobbyPage();
            popupMessage.DisplayPopupMessage("JOINED ROOM");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                string playerName = newPlayer.CustomProperties.ContainsKey("PlayerName") ?
                                    (string)newPlayer.CustomProperties["PlayerName"] : "Unknown";

                GameObject playerObject = PhotonNetwork.Instantiate(networkedPlayer.name, transform.position, Quaternion.identity);
                playerObject.GetComponent<NetworkedPlayer>().SetPlayerName(playerName);
            }
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