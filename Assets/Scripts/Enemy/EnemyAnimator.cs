using UnityEngine;

namespace Enemy {
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D rigidBody;

        private static readonly int horizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        private static readonly int speed = Animator.StringToHash("Speed");
        private static readonly int getHurt = Animator.StringToHash("GetHurt");

        public void GetHurt()
        {
            animator.SetTrigger(getHurt);
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 velocity = rigidBody.velocity;
            animator.SetFloat(horizontalSpeed, velocity.x);
            animator.SetFloat(speed, velocity.magnitude);
        }
    }
}
