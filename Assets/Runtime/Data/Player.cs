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
        private PlayerPosition positionOnCourt;

        private int gamesAssigned;
        private int gamesPlayed;
        private int wins;
        private int losses;
        private int deuceWins;
        private int deuceLosses;
        private int points;



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

        public PlayerPosition PositionOnCourt
        {
            get { return positionOnCourt; }
            set { positionOnCourt = value; }
        }



        // Static Stats
        public int GamesAssigned
        {
            get { return gamesAssigned; }
            set { gamesAssigned = value; }
        }
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

        public int Points
        {
            get { return points; }
            set { points = value; }
        }



        public int Score()
        {
            int score;

            switch (ConfigurationSettings.ScoringMethod)
            {
                case ScoringMethod.ScoreByWins:
                    score = (wins * 3) + (deuceWins * 2) + deuceLosses;
                    break;

                case ScoringMethod.ScoreByPoints:
                    score = points;
                    break;

                default:
                    Debug.LogWarning("Unsupported ScoringMethod");
                    score = -1;
                    break;
            }

            return score;          
        }
    }
}