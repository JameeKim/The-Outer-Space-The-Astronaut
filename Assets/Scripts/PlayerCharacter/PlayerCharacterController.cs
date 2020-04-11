using Combat;
using System.Collections;
using UnityEngine;

namespace PlayerCharacter {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CombatEntity))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [Min(1.0f)]
        public float acceleration = 5.0f;
        public float healthDecreasingTiming = 1.0f;
        public int heathDecreasingValue = 1;

        //Variables Related Power Up
        [SerializeField]
        private float powerUpTimer = 0.0f;
        private int powerUpValue = 0;
        private string typeOfItem = "DecreasePower";

        private Rigidbody2D rigidBody;
        private CombatEntity combatEntity;

        private bool disabled;
        private Coroutine currentCoroutine;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            combatEntity = GetComponent<CombatEntity>();

            //Initiate constantly decrease player health coroutine
            StartCoroutine(HealthDecreaseCoroutine(healthDecreasingTiming));
        }

        private void FixedUpdate()
        {
            if (disabled)
                return;

            float horiz = Input.GetAxisRaw("Horizontal");
            float vert = Input.GetAxisRaw("Vertical");

            Vector2 direction = new Vector2(horiz, vert).normalized;
            rigidBody.AddForce(direction * acceleration);

            //controller.DisableInputForSeconds(paralyzeDuration); // disable input for stalling effect

            PowerUpTimer(); // Consuming Caffeine adds time duration and runs its timer
        }
                
        public void PowerUpDuration(int amount, float duration)
        {
            this.powerUpValue += amount;
            this.powerUpTimer += duration;
        }

        private void PowerUpTimer()
        {
            if (powerUpTimer > 0)
            {
                powerUpTimer -= Time.fixedDeltaTime;
                
                if (powerUpTimer <= 0)
                {
                    combatEntity.GetPowerUp(typeOfItem, powerUpValue);
                    powerUpTimer = 0.0f;
                    powerUpValue = 0;
                }
            }
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
            currentCoroutine = StartCoroutine(HealthDecreaseCoroutine(healthDecreasingTiming));
        }

        private IEnumerator HealthDecreaseCoroutine(float seconds)
        {
            combatEntity.GetDecreased(heathDecreasingValue);
            yield return new WaitForSeconds(seconds);
            currentCoroutine = StartCoroutine(HealthDecreaseCoroutine(healthDecreasingTiming));
        }
    }
}
