using UnityEngine;

[System.Serializable]
public struct PlayerData
{
    public string playerName;
    public Sprite profileTexture;
    public Sprite bodyTexture;
}


namespace Runtime.Data
{
    public class Player
    {
        private string name;
        private Sprite profileSprite;
        private Sprite bodySprite;

        private int gamesPlayed;
        private int wins;
        private int losses;
        private int deuceWins;
        private int deuceLosses;



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
            get { return bodySprite; }
            set { bodySprite = value; }
        }



        // Static Stats
        public int GamesPlayed
        {
            get { return gamesPlayed; }
            set { gamesPlayed = value; }
        }
        public int Wins
        {
            get { return wins; }
            set { wins = value; }
        }
        public int Losses
        {
            get { return losses; }
            set { losses = value; }
        }
        public int DeuceWins
        {
            get { return deuceWins; }
            set { deuceWins = value; }
        }

        public int DeuceLosses
        {
            get { return deuceLosses; }
            set { deuceLosses = value; }
        }
    }
}


