using UnityEngine;
using Runtime.Data;
using Runtime.UI;

namespace Runtime.Managers
{
    public class ScoringManager : MonoBehaviour
    {
        [SerializeField] private UIManager uIManager;
        [SerializeField] private GameObject badmintonCourt;
        [SerializeField] private DisplayScoreController scoreDisplay;
        [SerializeField] private GenerateGameButtons gameButtonsDisplay;

        private Game currentGameData;
        private GameButtonController currentGameButton;


        private void Awake() => ValidateRequiredVariables();
        private void Start() => badmintonCourt.SetActive(false);

        public void SetCurrentGameData(Game game) => currentGameData = game;

        public void SetCurrentGameButton(GameButtonController gameButton) => currentGameButton = gameButton;

        public void OpenGame()
        {
            uIManager.ShowScoringPage();
            badmintonCourt.SetActive(true);
            scoreDisplay.SetTitle("GAME #" + currentGameData.Number.ToString());
            scoreDisplay.DisplayGamePreview();
        }
        public void StartGame()
        {
            scoreDisplay.DisplayGameScoring();
        }

        public void CompleteGame()
        {
            currentGameButton.ToggleGamePlayed(true);
            gameButtonsDisplay.UpdateContentHeight();
            CloseGame();
        }

        public void CloseGame()
        {
            uIManager.ShowGamesPage();
            badmintonCourt.SetActive(false);
            //gameTitle.text = " ";
        }




        private void ValidateRequiredVariables()
        {
            if (uIManager == null) { Debug.LogError("Null References: " + uIManager.name); }
            if (badmintonCourt == null) { Debug.LogError("Null References: " + badmintonCourt.name); }
            if (scoreDisplay == null) { Debug.LogError("Null References: " + scoreDisplay.name); }
            if (gameButtonsDisplay == null) { Debug.LogError("Null References: " + gameButtonsDisplay.name); }
        }
    }
}
