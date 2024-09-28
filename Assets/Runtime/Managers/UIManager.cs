using UnityEngine;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject homePage;
        [SerializeField] private GameObject setupPage;
        [SerializeField] private GameObject playersPage;
        [SerializeField] private GameObject gamesPage;
        [SerializeField] private GameObject scoringPage;
        [Space]
        [SerializeField] private GameObject notEnoughPlayersWarning;

        private void Awake() => ValidateRequiredVariables();

        private void Start()
        {
            HideAllPages();
            notEnoughPlayersWarning.SetActive(false);
            homePage.SetActive(true);         
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
        public void ShowGamesPage()
        {
            HideAllPages();
            gamesPage.SetActive(true);
        }
        public void ShowScoringPage()
        {
            HideAllPages();
            scoringPage.SetActive(true);
        }

        private void HideAllPages()
        {
            homePage.SetActive(false);
            setupPage.SetActive(false);
            playersPage.SetActive(false);
            gamesPage.SetActive(false);
            scoringPage.SetActive(false);
        }

        private void ValidateRequiredVariables()
        {
            if (homePage == null) { Debug.LogError("Null References: " + homePage.name); }
            if (setupPage == null) { Debug.LogError("Null References: " + setupPage.name); }
            if (playersPage == null) { Debug.LogError("Null References: " + playersPage.name); }
            if (gamesPage == null) { Debug.LogError("Null References: " + gamesPage.name); }
            if (scoringPage == null) { Debug.LogError("Null References: " + scoringPage.name); }
        }
    }
}