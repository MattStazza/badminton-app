using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public enum ScoringMethod
{
    ScoreByWins,
    ScoreByPoints
}

namespace Runtime.Data
{
    public static class ConfigurationSettings
    {
        // Private Variables
        private static ScoringMethod scoringMethod = ScoringMethod.ScoreByPoints;


        // Public Functions
        public static ScoringMethod ScoringMethod
        {
            get { return scoringMethod; }
            set { scoringMethod = value; }
        }
    }
}