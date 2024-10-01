using System.Collections;
using UnityEngine;
using TMPro;

namespace Runtime.UI
{
    public class PopupMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI message;
        [SerializeField] private float duration = 2f;

        private Vector3 initialScale;

        private void Awake() => ValidateRequiredVariables();

        private void Start()
        {       
            initialScale = message.transform.localScale;
            message.transform.localScale = Vector3.zero;
        }

        public void DisplayPopupMessage(string text)
        {
            message.text = text;
            StartMessageAnimation();
        }

        private void StartMessageAnimation() => StartCoroutine(AnimateMessage());

        private IEnumerator AnimateMessage()
        {
            yield return ScaleOverTime(Vector3.zero, initialScale, 0.25f);
            yield return new WaitForSeconds(duration);
            yield return ScaleOverTime(initialScale, Vector3.zero, 0.25f);
        }

        private IEnumerator ScaleOverTime(Vector3 from, Vector3 to, float time)
        {
            float elapsedTime = 0f;
            while (elapsedTime < time)
            {
                message.transform.localScale = Vector3.Lerp(from, to, elapsedTime / time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            message.transform.localScale = to;
        }

        private void ValidateRequiredVariables()
        {
            if (message == null)
            {
                Debug.LogError("Null References: " + message.name);
            }
        }
    }
}