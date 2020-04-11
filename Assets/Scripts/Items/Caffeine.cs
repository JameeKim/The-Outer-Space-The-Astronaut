using Combat;
using UnityEngine;

namespace Items {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable))]
    public class Caffeine : MonoBehaviour
    {
        public int powerUpAmount = 1;
        public float powerUpDuration = 10.0f;
        string typeOfItem = "Caffeine";

        private CombatEntity combatEntity;

        public void OnConsume(GameObject player)
        {
            combatEntity = player.GetComponent<CombatEntity>();
            combatEntity.GetPowerUp(typeOfItem, powerUpAmount); // GetPowerUp Needs parameters(String, Int)
        }
    }
}
