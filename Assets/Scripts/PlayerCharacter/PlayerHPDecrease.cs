using System.Collections;
using Combat;
using UnityEngine;

namespace PlayerCharacter {
    [RequireComponent(typeof(CombatEntity))]
    public class PlayerHPDecrease : MonoBehaviour
    {
        public int hpDecreasingValue = 1;
        public float decreasingInterval = 1.0f;

        private CombatEntity combatEntity;

        private void Start()
        {
            combatEntity = GetComponent<CombatEntity>();
            StartCoroutine(HealthDecreaseCoroutine(decreasingInterval));
        }

        private IEnumerator HealthDecreaseCoroutine(float interval)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(interval);

            while (true)
            {
                combatEntity.DecreaseHealth(hpDecreasingValue);
                yield return waitForSeconds;
            }
        }
    }
}
