using UnityEngine;
using System.Collections;

namespace Runtime.Utilities
{
    public class RotateWithEase : MonoBehaviour
    {
        [SerializeField] private float duration = 2f;
        [SerializeField] private float rotationAmount = 360f;
        [SerializeField] private bool antiClockwise = false;

        public void StartRotation() => StartCoroutine(Rotate(duration));

        IEnumerator Rotate(float duration)
        {
            if (antiClockwise)
                rotationAmount = rotationAmount * -1;

            float startRotation = transform.eulerAngles.y;
            float endRotation = startRotation + rotationAmount;
            float t = 0.0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                float yRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                yield return null;
            }
        }
    }
}
