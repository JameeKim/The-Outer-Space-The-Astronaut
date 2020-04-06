using Sirenix.OdinInspector;
using UnityEngine;

namespace PlayerCharacter {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCharacterAnimator : MonoBehaviour
    {
        [SceneObjectsOnly]
        [ValidateInput("MustBeSet")]
        public Animator spriteAnimator;

        private Rigidbody2D rigidBody;

        private static readonly int horizontal = Animator.StringToHash("Horizontal");
        private static readonly int speed = Animator.StringToHash("Speed");
        private static readonly int hurt = Animator.StringToHash("Hurt");

        /// <summary>
        ///   <para>Triggers the animation for the character being hurt.</para>
        /// </summary>
        public void GetHurt()
        {
            spriteAnimator.SetTrigger(hurt);
        }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 currentVelocity = rigidBody.velocity;
            spriteAnimator.SetFloat(horizontal, currentVelocity.x);
            spriteAnimator.SetFloat(speed, currentVelocity.magnitude);
        }

#if UNITY_EDITOR
        private bool MustBeSet(Animator value, ref string errorMsg)
        {
            bool isValid = value != null;
            if (!isValid)
            {
                errorMsg = "The field must be set to a non-null value";
            }
            return isValid;
        }
#endif
    }
}
