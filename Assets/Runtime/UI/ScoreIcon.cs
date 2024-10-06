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
        [SerializeField] private int winScore;
        [SerializeField] private int lossScore;
        [SerializeField] private int dueceWinScore;
        [SerializeField] private int dueceLossScore;
        [Space]
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private Image background;

        private void Awake() => ValidateRequiredVariables();

        public void DisplayScoreIcon(bool win, bool duece)
        {
            SetColor(win);

            if (duece)          
            {
                if (win)
                    score.text = "+" + dueceWinScore.ToString();
                else
                    score.text = "+" + dueceLossScore.ToString();

                return;
            }


            if (win)
                score.text = "+" + winScore.ToString();
            else
                score.text = "+" + lossScore.ToString();
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