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
            games = OrganiseOrderOfGames(games);
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

            // Ensure we have enough players to form teams
            if (playerCount < 4)
            {
                Debug.LogError("Not enough players to form teams.");
                return;
            }

            // Use a HashSet to keep track of unique team combinations
            HashSet<string> uniqueGames = new HashSet<string>();

            System.Random random = new System.Random(); // Random instance for shuffling

            // Generate all unique pairs of players
            for (int i = 0; i < playerCount; i++)
            {
                for (int j = i + 1; j < playerCount; j++)
                {
                    // Generate combinations for Team B from remaining players
                    for (int k = 0; k < playerCount; k++)
                    {
                        for (int l = k + 1; l < playerCount; l++)
                        {
                            if (k != i && k != j && l != i && l != j) // Ensure players are not from Team A
                            {
                                // Create pairs for the teams
                                var pair1 = new Dictionary<Player, Player> { { players[i], players[j] } };
                                var pair2 = new Dictionary<Player, Player> { { players[k], players[l] } };

                                // Randomly assign Team A and Team B
                                bool assignToTeamA = random.Next(0, 2) == 0;

                                var teamA = assignToTeamA ? pair1 : pair2;
                                var teamB = assignToTeamA ? pair2 : pair1;

                                // Create a unique identifier for the game
                                string teamIdentifier = $"{teamA.Keys.First().Name},{teamA.Values.First().Name} vs {teamB.Keys.First().Name},{teamB.Values.First().Name}";
                                string reverseIdentifier = $"{teamB.Keys.First().Name},{teamB.Values.First().Name} vs {teamA.Keys.First().Name},{teamA.Values.First().Name}";

                                // Check if the game is unique
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









        private List<Game> OrganiseOrderOfGames(List<Game> unorderedGames)
        {
            List<Game> orderedGames = new List<Game>();

            // Dictionary to track how many times a pair of players has been on the same team
            Dictionary<string, int> teamPairCount = new Dictionary<string, int>();

            // To track the most recent pairs to avoid consecutive pairings
            HashSet<string> recentPairs = new HashSet<string>();

            // Helper function to get a unique string key for a pair of players
            string GetTeamKey(Player p1, Player p2)
            {
                // Sort the players to ensure uniqueness (A, B) == (B, A)
                return string.Join(",", new List<string> { p1.Name, p2.Name }.OrderBy(name => name));
            }

            // Initialize the team pair count
            foreach (var game in unorderedGames)
            {
                string teamAKey = GetTeamKey(game.TeamA.Keys.First(), game.TeamA.Values.First());
                string teamBKey = GetTeamKey(game.TeamB.Keys.First(), game.TeamB.Values.First());

                teamPairCount[teamAKey] = 0;
                teamPairCount[teamBKey] = 0;
            }

            // Helper function to find if a game can be added based on pair distribution
            bool CanAddGame(Game game)
            {
                string teamAKey = GetTeamKey(game.TeamA.Keys.First(), game.TeamA.Values.First());
                string teamBKey = GetTeamKey(game.TeamB.Keys.First(), game.TeamB.Values.First());

                // Ensure no pair from the recent games is repeated
                if (recentPairs.Contains(teamAKey) || recentPairs.Contains(teamBKey))
                {
                    return false;
                }

                return true;
            }

            // Shuffle the games list to get a different order on each run
            System.Random random = new System.Random();
            unorderedGames = unorderedGames.OrderBy(x => random.Next()).ToList();

            // Iterate over the shuffled games and assign them based on pair priority
            while (unorderedGames.Count > 0)
            {
                bool gameAdded = false;

                for (int i = 0; i < unorderedGames.Count; i++)
                {
                    Game currentGame = unorderedGames[i];

                    if (CanAddGame(currentGame))
                    {
                        // Add the game to the ordered list
                        orderedGames.Add(currentGame);

                        // Update the pair counts for the teams
                        string teamAKey = GetTeamKey(currentGame.TeamA.Keys.First(), currentGame.TeamA.Values.First());
                        string teamBKey = GetTeamKey(currentGame.TeamB.Keys.First(), currentGame.TeamB.Values.First());

                        teamPairCount[teamAKey]++;
                        teamPairCount[teamBKey]++;

                        // Add the teams to the recent pairs set and keep it to the last game only
                        recentPairs.Clear();
                        recentPairs.Add(teamAKey);
                        recentPairs.Add(teamBKey);

                        // Remove the game from the remaining list
                        unorderedGames.RemoveAt(i);
                        gameAdded = true;

                        // Break the loop to restart the selection process
                        break;
                    }
                }

                // If no game could be added, just add all remaining games to the ordered list
                if (!gameAdded)
                {
                    orderedGames.AddRange(unorderedGames);
                    break;
                }
            }

            // Try to balance games per player here (without repeating pairs back-to-back)
            // At this point, we assume the game pairs are properly ordered and separated,
            // but if some player is underrepresented, we might add further logic to swap games.
            // However, this keeps the focus on the primary goal: avoiding consecutive pairs.

            return orderedGames;
        }




    }
}



