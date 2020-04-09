using System;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDetector : MonoBehaviour
    {
        public PlayerDetectorEvent onPlayerEnter;

        public PlayerDetectorEvent onPlayerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                onPlayerEnter.Invoke(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                onPlayerExit.Invoke(other.gameObject);
            }
        }

        [Serializable]
        public class PlayerDetectorEvent : UnityEvent<GameObject> {}
    }
}
