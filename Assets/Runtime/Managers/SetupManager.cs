using System.Collections.Generic;
using UnityEngine;
using Runtime.Data;
using System.Linq;
using System;


namespace Runtime.Managers
{
    public class SetupManager : MonoBehaviour
    {
        [SerializeField] private UIManager uIManager;

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
            {
                uIManager.ShowWarningNotEnoughPlayers();
                return;
            }

            FinalisePlayers();
            Session.Players = players;

            GenerateGames();
            games = BalancePairs(games);
            games = PrioritiseGamesPerPlayer(games, players);
            Session.Games = games;


            uIManager.ShowGamesPage();


            // Debug
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





        // Function to generate all unique combination of games
        private void GenerateGames()
        {
            int playerCount = players.Count;

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


        // Function to balance pairs and prevent back-to-back repeated pairs
        private List<Game> BalancePairs(List<Game> unorderedGames)
        {
            List<Game> balancedGames = new List<Game>();

            // Dictionary to track how many times a pair of players has been on the same team
            Dictionary<string, int> teamPairCount = new Dictionary<string, int>();

            // To track the most recent pairs to avoid consecutive pairings
            HashSet<string> recentPairs = new HashSet<string>();

            // Helper function to get a unique string key for a pair of players
            string GetTeamKey(Player p1, Player p2)
            {
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

            // Helper function to check if a game can be added based on pair repetition
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

            // Shuffle the games list for variation
            System.Random random = new System.Random();
            unorderedGames = unorderedGames.OrderBy(x => random.Next()).ToList();

            // Iterate through the shuffled games to prevent consecutive pairings
            while (unorderedGames.Count > 0)
            {
                bool gameAdded = false;

                for (int i = 0; i < unorderedGames.Count; i++)
                {
                    Game currentGame = unorderedGames[i];

                    if (CanAddGame(currentGame))
                    {
                        // Add the game to the balanced list
                        balancedGames.Add(currentGame);

                        // Update the pair counts for the teams
                        string teamAKey = GetTeamKey(currentGame.TeamA.Keys.First(), currentGame.TeamA.Values.First());
                        string teamBKey = GetTeamKey(currentGame.TeamB.Keys.First(), currentGame.TeamB.Values.First());

                        teamPairCount[teamAKey]++;
                        teamPairCount[teamBKey]++;

                        // Add teams to recent pairs set and clear recent pairs after one game
                        recentPairs.Clear();
                        recentPairs.Add(teamAKey);
                        recentPairs.Add(teamBKey);

                        // Remove game from unordered list
                        unorderedGames.RemoveAt(i);
                        gameAdded = true;

                        break;
                    }
                }

                // If no valid game was added, just add the rest
                if (!gameAdded)
                {
                    balancedGames.AddRange(unorderedGames);
                    break;
                }
            }

            return balancedGames;
        }


        // Function to prioritize games based on games played per player and ensure no player waits too long
        private List<Game> PrioritiseGamesPerPlayer(List<Game> balancedGames, List<Player> players)
        {
            // This will hold the newly ordered list of games
            List<Game> prioritisedGames = new List<Game>();

            // Dictionary to track how many games each player has played
            Dictionary<Player, int> playerGameCount = new Dictionary<Player, int>();

            // Dictionary to track when each player last played (game index)
            Dictionary<Player, int> playerLastGame = new Dictionary<Player, int>();

            // Initialize player game count and last game index to 0
            foreach (var player in players)
            {
                playerGameCount[player] = 0;
                playerLastGame[player] = -1; // Set to -1 since they haven't played any games yet
            }

            // Helper function to check if a game can be added based on current game distribution
            bool CanAddGame(Game game, int currentGameIndex)
            {
                // Get all players in this game
                var playersInGame = game.TeamA.Keys.Concat(game.TeamA.Values).Concat(game.TeamB.Keys).Concat(game.TeamB.Values).ToList();

                // Find the player who has played the fewest games
                int minGamesPlayed = playerGameCount.Values.Min();

                // Ensure that no player has sat out for more than two consecutive games
                foreach (var player in playersInGame)
                {
                    if (playerGameCount[player] > minGamesPlayed + 1)
                    {
                        return false; // If a player has played significantly more games, skip this game
                    }

                    // Ensure no player is sitting out for more than two games
                    if (currentGameIndex - playerLastGame[player] > 2)
                    {
                        return false;
                    }
                }

                return true;
            }

            // Create a copy of the balanced games to manipulate
            List<Game> remainingGames = new List<Game>(balancedGames);

            // Keep assigning games until we have processed all games
            int currentGameIndex = 0;

            while (remainingGames.Count > 0)
            {
                bool gameAdded = false;

                // Loop through the games to find one that can be added based on player game count and last game
                for (int i = 0; i < remainingGames.Count; i++)
                {
                    Game currentGame = remainingGames[i];

                    if (CanAddGame(currentGame, currentGameIndex))
                    {
                        // Add the game to the prioritised list
                        prioritisedGames.Add(currentGame);

                        // Update the game count and last game index for each player in the game
                        var playersInGame = currentGame.TeamA.Keys.Concat(currentGame.TeamA.Values).Concat(currentGame.TeamB.Keys).Concat(currentGame.TeamB.Values);
                        foreach (var player in playersInGame)
                        {
                            playerGameCount[player]++;
                            playerLastGame[player] = currentGameIndex;
                        }

                        // Remove the game from the remaining list
                        remainingGames.RemoveAt(i);
                        gameAdded = true;
                        currentGameIndex++; // Increment the game index

                        // Break the loop to restart the selection process
                        break;
                    }
                }

                // If no valid game could be added, add all remaining games to the prioritised list
                if (!gameAdded)
                {
                    prioritisedGames.AddRange(remainingGames);
                    break;
                }
            }

            return prioritisedGames;
        }

    }
}



