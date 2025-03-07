using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Runtime.Data;
using TMPro;

namespace Runtime.UI
{
    public class ScoreIcon : MonoBehaviour
    {
        [SerializeField] private Color winColor;
        [SerializeField] private Color lossColor;
        [Space]
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private Image background;

        private int WIN_SCORE = 3;
        private int LOSS_SCORE = 0;
        private int DUECE_WIN_SCORE = 2;
        private int DUECE_LOSS_SCORE = 1;

        private void Awake() => ValidateRequiredVariables();

        public void DisplayScoreIcon(bool win, bool duece, int points)
        {
            SetColor(win);

            switch (ConfigurationSettings.ScoringMethod)
            {
                case ScoringMethod.ScoreByWins:
                    DisplayScoreByWins(win, duece);
                    break;

                case ScoringMethod.ScoreByPoints:
                    DisplayScoreByPoints(points);
                    break;

                default:
                    Debug.LogWarning("Unsupported ScoringMethod");
                    break;
            }
        }

        private void DisplayScoreByPoints(int points)
        {
            score.text = "+" + points.ToString();
        }

        private void DisplayScoreByWins(bool win, bool duece)
        {
            if (duece)
            {
                if (win)
                    score.text = "+" + DUECE_WIN_SCORE.ToString();
                else
                    score.text = "+" + DUECE_LOSS_SCORE.ToString();

                return;
            }


            if (win)
                score.text = "+" + WIN_SCORE.ToString();
            else
                score.text = "+" + LOSS_SCORE.ToString();
        }

        private void SetColor(bool win)
        {
            if (win)
                background.color = winColor;
            else
                background.color = lossColor;
        }

        private void ValidateRequiredVariables()
        {
            if (score == null) { Debug.LogError("Null References: " + score.name); }
            if (background == null) { Debug.LogError("Null References: " + background.name); }
        }
    }
}