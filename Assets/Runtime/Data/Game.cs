using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Data
{
    public class Game
    {
        private int id;
        private int number;
        private float duration;
        private Dictionary<Player, Player> teamA = new Dictionary<Player, Player>();
        private int scoreA;
        private Dictionary<Player, Player> teamB = new Dictionary<Player, Player>();
        private int scoreB;


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
    }
}


