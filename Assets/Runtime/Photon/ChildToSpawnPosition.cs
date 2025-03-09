using UnityEngine;

namespace Runtime.Photon
{
    public class ChildToSpawnPosition : MonoBehaviour
    {
        private void Awake()
        {
            Transform spawnTransform = FindObjectOfType<SpawnPosition>().transform;

            if (spawnTransform != null)
                transform.SetParent(spawnTransform);
        }
    }
}