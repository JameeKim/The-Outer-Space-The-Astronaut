using UnityEngine;

namespace SpaceShip {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShipFlyAway : MonoBehaviour
    {
        [Min(0.1f)]
        public float acceleration = 10.0f;

        [Min(0.01f)]
        public float accelerationIncreaseDuration = 1.0f;

        private bool shouldGo;
        private float timeFromStart;

        private Rigidbody2D rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (!shouldGo)
                return;

            float realAcceleration = acceleration;
            if (timeFromStart < accelerationIncreaseDuration)
            {
                realAcceleration = Mathf.SmoothStep(0.0f, acceleration, timeFromStart / accelerationIncreaseDuration);
                timeFromStart += Time.fixedDeltaTime;
            }
            rigidBody.AddForce(Vector2.up * realAcceleration);
        }

        public void StartGoing()
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            shouldGo = true;
        }
    }
}
