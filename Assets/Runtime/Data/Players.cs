using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    [System.Serializable]
    public struct PlayerData
    {
        public string playerName;
        public Sprite profileTexture;
        public Sprite bodyTexture;
    }

    public class Players : MonoBehaviour
    {
        [SerializeField] private List<PlayerData> allPlayers;

        public List<PlayerData> GetAllPlayers()
        {
            return allPlayers;
        }
    }
}


