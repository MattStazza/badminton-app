using System.Collections;
using UnityEngine;

namespace Runtime.Utilities
{
    public class ObjectMover : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveDistance = 2.0f;
        [SerializeField] private Vector3 moveDirection = Vector3.down;
        [SerializeField] private float moveTime = 1.0f;
        [Space]
        [SerializeField] private bool returnToOriginalPosition;
        [SerializeField] private float stayTime = 2.0f;   

        private Vector3 _originalPosition;
        private bool _isMoving;

        private void Start() => _originalPosition = transform.position;

        public void MoveObject() => StartCoroutine(MoveDownAndBack());

        private IEnumerator MoveDownAndBack()
        {
            while (true)
            {
                if (_isMoving) yield break;

                _isMoving = true;

                yield return MoveToPosition(_originalPosition + moveDirection * moveDistance, moveTime);

                if (!returnToOriginalPosition) break;

                yield return new WaitForSeconds(stayTime);

                yield return MoveToPosition(_originalPosition, moveTime);

                _isMoving = false;
                break;
            }
        }

        private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
        {
            float elapsedTime = 0.0f;
            Vector3 startPosition = transform.position;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            transform.position = targetPosition;
        }
    }
}
