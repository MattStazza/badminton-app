using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Utilities
{

    [RequireComponent(typeof(Collider))]
    public class TouchOrClickEventTrigger : MonoBehaviour
    {
        // UnityEvent that will show up in the Inspector
        [SerializeField] private UnityEvent OnTouchOrClick;

        private void Awake()
        {
            if (OnTouchOrClick == null)
                OnTouchOrClick = new UnityEvent();
        }

        // For touch or click input detection
        private void OnMouseDown()
        {
            // Fire the event when the object is clicked or touched
            OnTouchOrClick.Invoke();
        }
    }
}