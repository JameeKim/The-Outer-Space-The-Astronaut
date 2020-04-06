using UnityEngine;

namespace PlayerCharacter {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [Min(0.0f)]
        public float maxSpeed = 10.0f;

        private Rigidbody2D rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float horiz = Input.GetAxis("Horizontal");
            float vert = Input.GetAxis("Vertical");

            // Map the square range of {(x,y) | -1 <= x,y <= 1} into unit circle {(x,y) | x^2 + y^2 <= 1}
            float normalizingFactor = Mathf.Max(Mathf.Abs(horiz), Mathf.Abs(vert));
            Vector2 direction= new Vector2(horiz, vert).normalized;
            Vector2 velocity = normalizingFactor * maxSpeed * direction;

            rigidBody.velocity = velocity;
        }
    }
}
