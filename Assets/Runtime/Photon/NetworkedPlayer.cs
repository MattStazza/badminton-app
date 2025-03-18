using TMPro;
using UnityEngine;

namespace Runtime.Photon
{
    public class NetworkedPlayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI username;

        private void Awake() => ValidateRequiredVariables();

        private void Start()
        {
            Transform spawnTransform = FindObjectOfType<NetworkedPlayerSpawnPosition>().transform;

            if (spawnTransform != null)
                transform.SetParent(spawnTransform);
        }

        public void SetPlayerName(string name) => username.text = name;

        private void ValidateRequiredVariables()
        {
            if (username == null) { Debug.LogError("Null References: " + username.name); }
        }
    }
}