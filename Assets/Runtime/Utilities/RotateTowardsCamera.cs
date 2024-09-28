using UnityEngine;

namespace Runtime.Utilities
{
    public class RotateTowardsCamera : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 2f;
        [SerializeField] private Vector3 rotationAxis = new Vector3(1,1,1);

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

                targetRotation.x = targetRotation.x * rotationAxis.x;
                targetRotation.y = targetRotation.y * rotationAxis.y;
                targetRotation.z = targetRotation.z * rotationAxis.z;

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }
        }
    }
}