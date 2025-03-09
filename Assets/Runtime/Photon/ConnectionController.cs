using UnityEngine;
using Photon.Pun;
using Runtime.Managers;

namespace Runtime.Photon
{
    public class ConnectionController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private UIManager uIManager;

        private void Awake() => ValidateRequiredVariables();

        private void Start() => PhotonNetwork.ConnectUsingSettings();

        public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();

        public override void OnJoinedLobby()
        {
            uIManager.ShowConnectedPage();
        }

        private void ValidateRequiredVariables()
        {
            if (uIManager == null) { Debug.LogError("Null References: " + uIManager.name); }
        }
    }
}