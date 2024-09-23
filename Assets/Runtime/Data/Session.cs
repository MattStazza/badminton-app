using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    public static class Session
    {
        // Private Variables
        private static int id;
        private static string date;
        private static List<Game> games;
        private static List<Player> players;
        private static float duration;


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

        public static float Duration
        {
            get { return duration; }
            set { duration = value; }
        }
    }
}

