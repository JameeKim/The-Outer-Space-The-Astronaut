using UnityEngine;

namespace Teleporters {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class TeleportedEntity : MonoBehaviour
    {
        private Rigidbody2D rigidBody;

        private bool isSent;

        public Rigidbody2D RigidBody => rigidBody;

        public bool IsSent
        {
            get
            {
                if (!isSent)
                    return false;

                isSent = false;
                return true;
            }
        }

        public void SetSent()
        {
            isSent = true;
        }

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }
    }
}
