using System.Collections;
using UnityEngine;

namespace PlayerCharacter {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [Min(1.0f)]
        public float acceleration = 5.0f;

        private Rigidbody2D rigidBody;

        private bool disabled;
        private Coroutine currentCoroutine;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (disabled)
                return;

            float horiz = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");

            Vector2 direction= new Vector2(horiz, vert).normalized;
            rigidBody.AddForce(direction * acceleration);
        }

        public void DisableInputForSeconds(float seconds)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            currentCoroutine = StartCoroutine(DisableInputCoroutine(seconds));
        }

        private IEnumerator DisableInputCoroutine(float seconds)
        {
            disabled = true;
            yield return new WaitForSeconds(seconds);
            disabled = false;
            currentCoroutine = null;
        }
    }
}
