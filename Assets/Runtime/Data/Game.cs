using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    public class Game
    {
        private int id;
        private Dictionary<int, int> score = new Dictionary<int, int>();
        private Dictionary<Player, Player> teamA = new Dictionary<Player, Player>();
        private Dictionary<Player, Player> teamB = new Dictionary<Player, Player>();
        private float duration;


        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public Dictionary<int, int> Score
        {
            get { return score; }
            set { score = value; }
        }

        public Dictionary<Player, Player> TeamA
        {
            get { return teamA; }
            set { teamA = value; }
        }

        public Dictionary<Player, Player> TeamB
        {
            get { return teamB; }
            set { teamB = value; }
        }

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }
    }
}


