using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    public class Players : MonoBehaviour
    {
        [SerializeField] private List<PlayerData> allPlayers;

        public List<PlayerData> GetAllPlayerData()
        {
            return allPlayers;
        }
    }
}


