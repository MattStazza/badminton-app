using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Runtime.Data;

namespace Runtime.UI.Results
{
    public class SessionResultsController : MonoBehaviour
    {
        [SerializeField] private PodiumController podium;
        [SerializeField] private ScoreboardController scoreboard;
        [SerializeField] private TextMeshProUGUI gamesPlayed;

        private void Awake() => ValidateRequiredVariables();

        private void OnEnable() => DisplayResults();


        public void DisplayResults()
        {
            podium.SetupPodium();
            scoreboard.SetupScoreboard();
            gamesPlayed.text = GetCompletedGames();
        }



        private string GetCompletedGames()
        {
            int count = 0;
    
            foreach (Game game in Session.Games)
            {
                if (game.Complete)
                    count++;
            }

            if (count < 10)
                return "0" + count.ToString();
            else
                return count.ToString();
        } 




        private void ValidateRequiredVariables()
        {
            if (podium == null) { Debug.LogError("Null References: " + podium.name); }
            if (scoreboard == null) { Debug.LogError("Null References: " + scoreboard.name); }
            if (gamesPlayed == null) { Debug.LogError("Null References: " + gamesPlayed.name); }
        }
    }
}