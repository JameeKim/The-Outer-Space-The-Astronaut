using System;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShip {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class PlayerCollideDetector : MonoBehaviour
    {
        public PlayerCollideEvent onPlayerCollide;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
                onPlayerCollide.Invoke(other.gameObject);
        }

        [Serializable]
        public class PlayerCollideEvent : UnityEvent<GameObject> {}
    }
}
