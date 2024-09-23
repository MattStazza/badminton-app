using UnityEngine;


namespace Runtime.Data
{
    public class Player : MonoBehaviour
    {
        private static string name;
        private static Sprite profileSprite;
        private static Sprite bodyTexture;
        private static int gamesPlayed;
        private static int gamesWon;
        private static int gamesLost;
        private static int gamesDeuced;



        public static string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static Sprite ProfileSprite
        {
            get { return profileSprite; }
            set { profileSprite = value; }
        }
        public static Sprite BodySprite
        {
            get { return bodyTexture; }
            set { bodyTexture = value; }
        }



        // Stats
        public static int GamesPlayed
        {
            get { return gamesPlayed; }
            set { gamesPlayed = value; }
        }
        public static int GamesWon
        {
            get { return gamesWon; }
            set { gamesWon = value; }
        }
        public static int GamesLost
        {
            get { return gamesLost; }
            set { gamesLost = value; }
        }
        public static int GamesDeuced
        {
            get { return gamesDeuced; }
            set { gamesDeuced = value; }
        }
    }
}


