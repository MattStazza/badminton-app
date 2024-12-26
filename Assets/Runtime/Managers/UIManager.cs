using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject homePage;
        [SerializeField] private GameObject setupPage;
        [SerializeField] private GameObject playersPage;
        [SerializeField] private GameObject allGamesPage;
        [SerializeField] private GameObject gamePage;
        [SerializeField] private GameObject scoringPage;
        [SerializeField] private GameObject gameResultsPage;
        [SerializeField] private GameObject sessionResultsPage;
        [Space]
        [SerializeField] private GameObject popupMessage;
        [SerializeField] private GameObject notEnoughPlayersWarning;

        private void Awake() => ValidateRequiredVariables();

        private void Start()
        {
            HideAllPages();
            notEnoughPlayersWarning.SetActive(false);
            homePage.SetActive(true);
            popupMessage.SetActive(true);
        }

        public void ShowSetupPage()
        {
            HideAllPages();
            setupPage.SetActive(true);
        }
        public void ShowWarningNotEnoughPlayers()
        {
            notEnoughPlayersWarning.SetActive(true);
        }

        public void ShowPlayersPage()
        {
            HideAllPages();
            playersPage.SetActive(true);
            notEnoughPlayersWarning.SetActive(false);
        }
        public void ShowAllGamesPage()
        {
            HideAllPages();
            allGamesPage.SetActive(true);
        }
        public void ShowGamePage()
        {
            HideAllPages();
            gamePage.SetActive(true);
        }
        public void ShowScoringPage()
        {
            HideAllPages();
            scoringPage.SetActive(true);
        }
        public void ShowGameResultsPage()
        {
            HideAllPages();
            gameResultsPage.SetActive(true);
        }
        public void ShowSessionResultsPage()
        {
            HideAllPages();
            sessionResultsPage.SetActive(true);
        }

        private void HideAllPages()
        {
            homePage.SetActive(false);
            setupPage.SetActive(false);
            playersPage.SetActive(false);
            allGamesPage.SetActive(false);
            gamePage.SetActive(false);
            scoringPage.SetActive(false);
            gameResultsPage.SetActive(false);
            sessionResultsPage.SetActive(false);
        }

        private void ValidateRequiredVariables()
        {
            if (homePage == null) { Debug.LogError("Null References: " + homePage.name); }
            if (setupPage == null) { Debug.LogError("Null References: " + setupPage.name); }
            if (playersPage == null) { Debug.LogError("Null References: " + playersPage.name); }
            if (allGamesPage == null) { Debug.LogError("Null References: " + allGamesPage.name); }
            if (gamePage == null) { Debug.LogError("Null References: " + gamePage.name); }
            if (scoringPage == null) { Debug.LogError("Null References: " + scoringPage.name); }
            if (gameResultsPage == null) { Debug.LogError("Null References: " + gameResultsPage.name); }
            if (sessionResultsPage == null) { Debug.LogError("Null References: " + sessionResultsPage.name); }
            if (popupMessage == null) { Debug.LogError("Null References: " + popupMessage.name); }
            if (notEnoughPlayersWarning == null) { Debug.LogError("Null References: " + notEnoughPlayersWarning.name); }
        }
    }
}