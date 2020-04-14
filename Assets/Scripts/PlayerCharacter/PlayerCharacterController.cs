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
        private bool forcedMove;
        private Vector2 forcedMoveDirection;
        private Coroutine disableInputCoroutine;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (disabled)
                return;

            Vector2 direction = forcedMove
                ? forcedMoveDirection
                : new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            rigidBody.AddForce(direction * acceleration);
        }

        public void DisableInputForSeconds(float seconds)
        {
            if (disableInputCoroutine != null)
                StopCoroutine(disableInputCoroutine);

            disableInputCoroutine = StartCoroutine(DisableInputCoroutine(seconds));
        }

        public void DisableInputAndMoveForSeconds(float seconds, Vector2 direction)
        {
            if (disableInputCoroutine != null)
                StopCoroutine(disableInputCoroutine);

            disableInputCoroutine = StartCoroutine(DisableInputAndMoveCoroutine(seconds, direction));
        }

        private IEnumerator DisableInputCoroutine(float seconds)
        {
            disabled = true;
            forcedMove = false;
            yield return new WaitForSeconds(seconds);
            disabled = false;
            disableInputCoroutine = null;
        }

        private IEnumerator DisableInputAndMoveCoroutine(float seconds, Vector2 direction)
        {
            disabled = false;
            forcedMove = true;
            forcedMoveDirection = direction.normalized;
            yield return new WaitForSeconds(seconds);
            forcedMove = false;
            disableInputCoroutine = null;
        }
    }
}
