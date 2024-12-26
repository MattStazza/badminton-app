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
            games = PrioritizeGamesPerPlayer(games, players);
            Session.Games = games;

            uIManager.ShowAllGamesPage();
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
            Dictionary<string, int> teamPairCount = new Dictionary<string, int>();
            HashSet<string> recentPairs = new HashSet<string>();


            string GetTeamKey(Player p1, Player p2)
            {
                return string.Join(",", new List<string> { p1.Name, p2.Name }.OrderBy(name => name));
            }

            foreach (var game in unorderedGames)
            {
                string teamAKey = GetTeamKey(game.TeamA.Keys.First(), game.TeamA.Values.First());
                string teamBKey = GetTeamKey(game.TeamB.Keys.First(), game.TeamB.Values.First());

                teamPairCount[teamAKey] = 0;
                teamPairCount[teamBKey] = 0;
            }

            bool CanAddGame(Game game)
            {
                string teamAKey = GetTeamKey(game.TeamA.Keys.First(), game.TeamA.Values.First());
                string teamBKey = GetTeamKey(game.TeamB.Keys.First(), game.TeamB.Values.First());

                if (recentPairs.Contains(teamAKey) || recentPairs.Contains(teamBKey))
                    return false;

                return true;
            }

            System.Random random = new System.Random();
            unorderedGames = unorderedGames.OrderBy(x => random.Next()).ToList();

            while (unorderedGames.Count > 0)
            {
                bool gameAdded = false;

                for (int i = 0; i < unorderedGames.Count; i++)
                {
                    Game currentGame = unorderedGames[i];

                    if (CanAddGame(currentGame))
                    {
                        balancedGames.Add(currentGame);

                        string teamAKey = GetTeamKey(currentGame.TeamA.Keys.First(), currentGame.TeamA.Values.First());
                        string teamBKey = GetTeamKey(currentGame.TeamB.Keys.First(), currentGame.TeamB.Values.First());

                        teamPairCount[teamAKey]++;
                        teamPairCount[teamBKey]++;

                        recentPairs.Clear();
                        recentPairs.Add(teamAKey);
                        recentPairs.Add(teamBKey);

                        unorderedGames.RemoveAt(i);
                        gameAdded = true;

                        break;
                    }
                }

                if (!gameAdded)
                {
                    balancedGames.AddRange(unorderedGames);
                    break;
                }
            }

            return balancedGames;
        }


        private List<Game> PrioritizeGamesPerPlayer(List<Game> balancedGames, List<Player> players)
        {
            List<Game> prioritizedGames = new List<Game>();
            HashSet<Game> remainingGames = new HashSet<Game>(balancedGames);

            while (remainingGames.Count > 0)
            {
                bool gameAddedInThisIteration = false;

                foreach (Game game in remainingGames.ToList())
                {
                    List<Player> gamePlayers = GetLowestAssignedPlayers(players);

                    if (GameIncludesPlayers(game, gamePlayers))
                    {
                        prioritizedGames.Add(game);
                        IncrementGamesAssigned(gamePlayers);
                        remainingGames.Remove(game);
                        gameAddedInThisIteration = true; 
                    }
                }

                if (!gameAddedInThisIteration)
                    break;
            }

            return prioritizedGames;
        }

        private bool GameIncludesPlayers(Game game, List<Player> players)
        {
            bool hasPlayers = false;
            List<String> playerNames = new List<String>();

            foreach (KeyValuePair<Player, Player> player in game.TeamA)
            { playerNames.Add(player.Key.Name); playerNames.Add(player.Value.Name); }

            foreach (KeyValuePair<Player, Player> player in game.TeamB)
            { playerNames.Add(player.Key.Name); playerNames.Add(player.Value.Name); }

            foreach (Player player in players)
            {
                if (playerNames.Contains(player.Name))
                    hasPlayers = true;
                else
                {
                    hasPlayers = false;
                    break;
                }                
            }

            return hasPlayers;
        }

        private void IncrementGamesAssigned(List<Player> players)
        {
            foreach (Player player in players)
            {
                player.GamesAssigned++;
            }
        }

        private List<Player> GetLowestAssignedPlayers(List<Player> allPlayers) 
        {
            List<Player> shuffledPlayers = ShufflePlayers(allPlayers);

            List<Player> lowestAssignedPlayers = shuffledPlayers
                .OrderBy(p => p.GamesAssigned)
                .Take(4)
                .ToList();

            return lowestAssignedPlayers;
        }

        private List<Player> ShufflePlayers(List<Player> players)
        {
            System.Random rng = new System.Random();
            return players.OrderBy(p => rng.Next()).ToList();
        }
    }
}