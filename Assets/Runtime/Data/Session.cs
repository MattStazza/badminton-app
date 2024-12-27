using System.Collections.Generic;
using System.Linq;


[System.Serializable]
public enum TopThreeRanking
{
    FirstFirstFirst,
    FirstFirstSecond,
    FirstSecondSecond,
    FirstSecondThird
}


namespace Runtime.Data
{
    public static class Session
    {
        // Private Variables
        private static int id;
        private static string date;
        private static Game currentGame;
        private static List<Game> games;
        private static List<Player> players;
        private static string duration;


        // Public Functions
        public static int ID
        {
            get { return id; }
            set { id = value; }
        }

        public static string Date
        {
            get { return date; }
            set { date = value; }
        }

        public static Game CurrentGame
        {
            get { return currentGame; }
            set { currentGame = value; }
        }

        public static List<Game> Games
        {
            get { return games; }
            set { games = value; }
        }

        public static List<Player> Players
        {
            get { return players; }
            set { players = value; }
        }

        public static string Duration
        {
            get { return duration; }
            set { duration = value; }
        }


        public static List<Player> GetTopThreePlayers()
        {
            List<Player> topThreePlayers = new List<Player>();

            topThreePlayers = players
                .OrderByDescending(player => player.Score())
                .ThenBy(player => player.GamesPlayed)
                .Take(3)
                .ToList();

            return topThreePlayers;
        }


        public static TopThreeRanking DetermineTopThreeRanking()
        {
            List<Player> topThreePlayers = GetTopThreePlayers();

            if (topThreePlayers.Count < 3)
                return TopThreeRanking.FirstSecondThird;

            bool firstPlaceTie = topThreePlayers[0].Score() == topThreePlayers[1].Score();
            bool secondPlaceTie = topThreePlayers[1].Score() == topThreePlayers[2].Score();
            bool firstPlaceTieEqualGames = topThreePlayers[0].GamesPlayed == topThreePlayers[1].GamesPlayed;
            bool secondPlaceTieEqualGames = topThreePlayers[1].GamesPlayed == topThreePlayers[2].GamesPlayed;

            if (firstPlaceTie && secondPlaceTie)
            {
                if (firstPlaceTieEqualGames && secondPlaceTieEqualGames)
                    return TopThreeRanking.FirstFirstFirst;

                if (firstPlaceTieEqualGames)
                    return TopThreeRanking.FirstFirstSecond;

                if (secondPlaceTieEqualGames)
                    return TopThreeRanking.FirstSecondSecond;

                return TopThreeRanking.FirstSecondThird;
            }

            if (firstPlaceTie)
                return firstPlaceTieEqualGames ? TopThreeRanking.FirstFirstSecond : TopThreeRanking.FirstSecondThird;

            if (secondPlaceTie)
                return secondPlaceTieEqualGames ? TopThreeRanking.FirstSecondSecond : TopThreeRanking.FirstSecondThird;

            return TopThreeRanking.FirstSecondThird;
        }
    }
}