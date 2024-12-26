using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Runtime.UI.Results
{
    public class StandController : MonoBehaviour
    {

        [SerializeField] private StandLevel level;
        [Space]
        [SerializeField] private TextMeshProUGUI number;
        [SerializeField] private PlayerOnCourt playerOnStand;
        [SerializeField] private Image standImage;
        [SerializeField] private Image platformImage;
        [SerializeField] private RectTransform rectTransform;
        [Space]
        [SerializeField] private Color firstColor;
        [SerializeField] private Color secondColor;
        [SerializeField] private Color thirdColor;

        private float WIDTH = 200f;
        private float FIRST_HEGIHT = 350f;
        private float SECOND_HEGIHT = 200f;
        private float THIRD_HEGIHT = 100f;


        private void Awake() => ValidateRequiredVariables();


        public void SetupStand(StandLevel level)
        {
            switch (level)
            {
                case StandLevel.First:
                    number.text = "1ST";
                    standImage.color = SetAlpha(firstColor, 0.5f); 
                    platformImage.color = SetAlpha(firstColor, 1f);
                    rectTransform.sizeDelta = new Vector2 (WIDTH, FIRST_HEGIHT);
                    break;

                case StandLevel.Second:
                    number.text = "2ND";
                    standImage.color = SetAlpha(secondColor, 0.5f);
                    platformImage.color = SetAlpha(secondColor, 1f);
                    rectTransform.sizeDelta = new Vector2(WIDTH, SECOND_HEGIHT);
                    break;

                case StandLevel.Third:
                    number.text = "3RD";
                    standImage.color = SetAlpha(thirdColor, 0.5f);
                    platformImage.color = SetAlpha(thirdColor, 1f);
                    rectTransform.sizeDelta = new Vector2(WIDTH, THIRD_HEGIHT);
                    break;

                default:
                    Debug.LogWarning("Unsupported StandLevel: " + level);
                    break;
            }
        }

        private Color SetAlpha(Color color, float alpha)
        {
            color.a = Mathf.Clamp01(alpha); 
            return color;
        }

        private void ValidateRequiredVariables()
        {
            if (number == null) { Debug.LogError("Null References: " + number.name); }
            if (playerOnStand == null) { Debug.LogError("Null References: " + playerOnStand.name); }
            if (standImage == null) { Debug.LogError("Null References: " + standImage.name); }
            if (platformImage == null) { Debug.LogError("Null References: " + platformImage.name); }
            if (rectTransform == null) { Debug.LogError("Null References: " + rectTransform.name); }
        }
    }
}