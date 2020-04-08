using UnityEngine;

namespace PlayerCharacter {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [Min(1.0f)]
        public float acceleration = 5.0f;

        private Rigidbody2D rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float horiz = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");

            Vector2 direction= new Vector2(horiz, vert).normalized;
            rigidBody.AddForce(direction * acceleration);
        }
    }
}
