namespace Runtime.Data
{
    public enum PlayerPosition
    {
        Right,
        Left
    } 

    public class Round
    {
        private int scoreA;
        private int scoreB;

        // Team A
        private PlayerPosition player1Position;
        private PlayerPosition player2Position;

        // Team B
        private PlayerPosition player3Position;
        private PlayerPosition player4Position;

        public int ScoreA
        {
            get { return scoreA; }
            set { scoreA = value; }
        }

        public int ScoreB
        {
            get { return scoreB; }
            set { scoreB = value; }
        }

        public PlayerPosition Player1Position
        {
            get { return player1Position; }
            set { player1Position = value; }
        }

        public PlayerPosition Player2Position
        {
            get { return player2Position; }
            set { player2Position = value; }
        }

        public PlayerPosition Player3Position
        {
            get { return player3Position; }
            set { player3Position = value; }
        }

        public PlayerPosition Player4Position
        {
            get { return player4Position; }
            set { player4Position = value; }
        }
    }
}