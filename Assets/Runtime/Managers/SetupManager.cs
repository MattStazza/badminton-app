using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;

namespace Runtime.Managers
{
    public class SetupManager : MonoBehaviour
    {
        public List<PlayerData> selectedPlayers = new List<PlayerData>();

        public List<PlayerData> GetSelectedPlayers() { return selectedPlayers; }

        public void ClearSelectedPlayers() => selectedPlayers.Clear();

        public void AddPlayerToSelected(PlayerData selectedPlayer)
        {
            selectedPlayers.Add(selectedPlayer);
        }



        public void GenerateGames()
        {
            if (selectedPlayers.Count <= 3)
            {
                Debug.Log("Not enough players to generate games!");
            }
        }
    }
}


