using System.Collections.Generic;

namespace Runtime.Data
{
    public class Game
    {
        private int id;
        private int number;
        private float duration;
        private List<Round> rounds;
        private Dictionary<Player, Player> teamA = new Dictionary<Player, Player>();
        private int scoreA;
        private Dictionary<Player, Player> teamB = new Dictionary<Player, Player>();
        private int scoreB;
        private bool played;


        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        } 

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        public List<Round> Rounds
        {
            get { return rounds; }
            set { rounds = value; }
        }

        public Dictionary<Player, Player> TeamA
        {
            get { return teamA; }
            set { teamA = value; }
        }

        public int ScoreA
        {
            get { return scoreA; }
            set { scoreA = value; }
        }

        public Dictionary<Player, Player> TeamB
        {
            get { return teamB; }
            set { teamB = value; }
        }

        public int ScoreB
        {
            get { return scoreB; }
            set { scoreB = value; }
        }

        public bool Played
        {
            get { return played; }
            set { played = value; }
        }
    }
}


