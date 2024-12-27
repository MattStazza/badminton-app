using UnityEngine;
using Runtime.Data;
using System.Collections.Generic;
using System.Linq;

namespace Runtime.UI.Results
{
    public class ScoreboardController : MonoBehaviour
    {
        [SerializeField] private GameObject title;
        [SerializeField] private GameObject information;
        [SerializeField] private GameObject slotPrefab;
        [Space]
        [SerializeField] private Color firstColor;
        [SerializeField] private Color secondColor;
        [SerializeField] private Color thirdColor;

        private List<SlotController> slots = new List<SlotController>();

        private void Awake() => ValidateRequiredVariables();

        public void SetupScoreboard()
        {
            ClearExistingSlots();

            foreach (Player player in Session.Players)
                SpawnPlayerSlot(player);

            OrderSlots();
            ColorTopThreeSlots();
        }

        private void SpawnPlayerSlot(Player player)
        {
            GameObject slot = Instantiate(slotPrefab, transform);
            SlotController slotController = slot.GetComponent<SlotController>();
            slotController.DisplayPlayerStats(player);
            slots.Add(slotController);
        }

        private void OrderSlots()
        {
            var orderedSlots = slots
                .OrderByDescending(slot => slot.GetSlotScore()) 
                .ThenBy(slot => slot.GetSlotGames())            
                .ToList();

            slots = orderedSlots;

            for (int i = 0; i < orderedSlots.Count; i++)
                orderedSlots[i].transform.SetSiblingIndex(i);

            title.transform.SetSiblingIndex(0);
            information.transform.SetSiblingIndex(1);
        }

        private void ColorTopThreeSlots()
        {
            TopThreeRanking ranking = Session.DetermineTopThreeRanking();

            switch (ranking)
            {
                case TopThreeRanking.FirstFirstFirst:
                    slots[0].SetBackgroundColor(firstColor);
                    slots[1].SetBackgroundColor(firstColor);
                    slots[2].SetBackgroundColor(firstColor);
                    break;

                case TopThreeRanking.FirstFirstSecond:
                    slots[0].SetBackgroundColor(firstColor);
                    slots[1].SetBackgroundColor(firstColor);
                    slots[2].SetBackgroundColor(secondColor);
                    break;

                case TopThreeRanking.FirstSecondSecond:
                    slots[0].SetBackgroundColor(firstColor);
                    slots[1].SetBackgroundColor(secondColor);
                    slots[2].SetBackgroundColor(secondColor);
                    break;

                case TopThreeRanking.FirstSecondThird:
                    slots[0].SetBackgroundColor(firstColor);
                    slots[1].SetBackgroundColor(secondColor);
                    slots[2].SetBackgroundColor(thirdColor);
                    break;

                default:
                    Debug.LogWarning("Unsupported TopThreeRanking: " + ranking);
                    break;
            }
        }

        private void ClearExistingSlots()
        {
            if (slots.Count == 0)
                return;

            foreach (SlotController slot in slots)
                Destroy(slot.gameObject);

            slots.Clear();
        }

        private void ValidateRequiredVariables()
        {
            if (title == null) { Debug.LogError("Null References: " + title.name); }
            if (information == null) { Debug.LogError("Null References: " + information.name); }
            if (slotPrefab == null) { Debug.LogError("Null References: " + slotPrefab.name); }
        }
    }
}