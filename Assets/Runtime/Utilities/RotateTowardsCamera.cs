using UnityEngine;

namespace Runtime.Utilities
{
    public class RotateTowardsCamera : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 2f;

        private Transform cameraTransform;

        private void Awake() => cameraTransform = Camera.main.transform;

        void Update()
        {
            if (cameraTransform == null)
                return;

            Vector3 directionToCamera = cameraTransform.position - transform.position;

            if (directionToCamera != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
        }
    }
}