using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;
using System.Linq;
using System;


namespace Runtime.Managers
{
    public class SetupManager : MonoBehaviour
    {
        // Data to send to Session
        private List<Player> players = new List<Player>();
        private List<Game> games = new List<Game>();


        // Selected Players
        private List<PlayerData> selectedPlayersData = new List<PlayerData>();
        public List<PlayerData> GetSelectedPlayers() { return selectedPlayersData; }
        public void ClearSelectedPlayers() => selectedPlayersData.Clear();
        public void AddPlayerToSelected(PlayerData selectedPlayer) => selectedPlayersData.Add(selectedPlayer);



        public void GenerateSession()
        {
            if (selectedPlayersData.Count <= 3)
                Debug.Log("Not enough players to generate games!");


            FinalisePlayers();
            Session.Players = players;

            GenerateGames();
            Session.Games = games;



            foreach (Game game in Session.Games)
            {
                // Create a summary log for the game
                string teamASummary = string.Join(", ", game.TeamA.Select(pair => $"{pair.Key.Name} & {pair.Value.Name}"));
                string teamBSummary = string.Join(", ", game.TeamB.Select(pair => $"{pair.Key.Name} & {pair.Value.Name}"));
                Debug.Log($"Game ID: {game.ID} | Team A: [{teamASummary}] | Team B: [{teamBSummary}]");
            }

        }




        private void FinalisePlayers()
        {
            // For every selected player create a Player & set data.
            for (int i = 0; i < selectedPlayersData.Count; i++)
            {
                PlayerData data = selectedPlayersData[i];
                Player player = new Player();

                player.Name = data.playerName;
                player.ProfileSprite = data.profileTexture;
                player.BodySprite = data.bodyTexture;

                players.Add(player);
            }
        }






    private void GenerateGames()
        {
            int playerCount = players.Count;

            // Ensure we have an even number of players for pairing
            if (playerCount < 4)
            {
                Debug.LogError("Not enough players to form teams.");
                return;
            }

            // Use a HashSet to keep track of unique team combinations
            HashSet<string> uniqueGames = new HashSet<string>();

            // Generate all unique pairs of players
            for (int i = 0; i < playerCount; i++)
            {
                for (int j = i + 1; j < playerCount; j++)
                {
                    // Team A
                    var teamA = new Dictionary<Player, Player> { { players[i], players[j] } };

                    // Generate combinations for Team B from remaining players
                    for (int k = 0; k < playerCount; k++)
                    {
                        for (int l = k + 1; l < playerCount; l++)
                        {
                            if (k != i && k != j && l != i && l != j) // Ensure players are not from Team A
                            {
                                var teamB = new Dictionary<Player, Player> { { players[k], players[l] } };

                                // Create a unique identifier for the game
                                string teamIdentifier = $"{players[i].Name},{players[j].Name} vs {players[k].Name},{players[l].Name}";

                                // Check if the reverse combination exists
                                string reverseIdentifier = $"{players[k].Name},{players[l].Name} vs {players[i].Name},{players[j].Name}";

                                if (!uniqueGames.Contains(teamIdentifier) && !uniqueGames.Contains(reverseIdentifier))
                                {
                                    uniqueGames.Add(teamIdentifier);

                                    Game game = new Game
                                    {
                                        ID = games.Count + 1,
                                        TeamA = teamA,
                                        TeamB = teamB
                                    };

                                    games.Add(game);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}



