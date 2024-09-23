using UnityEngine;


namespace Runtime.Data
{
    public class Player
    {
        private string name;
        private Sprite profileSprite;
        private Sprite bodyTexture;

        private int gamesPlayed;
        private int gamesWon;
        private int gamesLost;
        private int gamesDeuced;



        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Sprite ProfileSprite
        {
            get { return profileSprite; }
            set { profileSprite = value; }
        }

        public Sprite BodySprite
        {
            get { return bodyTexture; }
            set { bodyTexture = value; }
        }



        // Static Stats
        public int GamesPlayed
        {
            get { return gamesPlayed; }
            set { gamesPlayed = value; }
        }
        public int GamesWon
        {
            get { return gamesWon; }
            set { gamesWon = value; }
        }
        public int GamesLost
        {
            get { return gamesLost; }
            set { gamesLost = value; }
        }
        public int GamesDeuced
        {
            get { return gamesDeuced; }
            set { gamesDeuced = value; }
        }
    }
}


