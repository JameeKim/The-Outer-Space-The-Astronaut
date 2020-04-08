using UnityEngine;

namespace Enemy {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyController : MonoBehaviour
    {
        public float chaseFactor = 5.0f;

        private Rigidbody2D rigidBody;

        private GameObject target;
        private Transform targetTransform;

        public GameObject Target
        {
            get => target;
            set
            {
                target = value;
                targetTransform = target != null ? target.transform : null;
            }
        }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (targetTransform == null)
                return; // don't move if there is no target

            Vector2 displacement = (Vector2)targetTransform.position - rigidBody.position;
            rigidBody.AddForce(displacement.normalized * chaseFactor);
        }
    }
}
