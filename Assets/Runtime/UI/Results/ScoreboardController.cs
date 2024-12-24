using UnityEngine;
using Runtime.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

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
            var orderedSlots = slots.OrderByDescending(slot => slot.GetSlotScore()).ToList();

            for (int i = 0; i < orderedSlots.Count; i++)
            {
                orderedSlots[i].transform.SetSiblingIndex(i); 
            }

            title.transform.SetSiblingIndex(0);
            information.transform.SetSiblingIndex(1);

            // Color Top 3
            transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().color = firstColor;
            transform.GetChild(3).transform.GetChild(0).GetComponent<Image>().color = secondColor;
            transform.GetChild(4).transform.GetChild(0).GetComponent<Image>().color = thirdColor;
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