using Combat;
using PlayerCharacter;
using UnityEngine;

namespace Items {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable))]
    public class Caffeine : MonoBehaviour
    {
        //makes player power up temporary
        public int powerUpAmount = 2;
        public float powerUpDuration = 5.0f;
        string typeOfItem = "Caffeine";

        private CombatEntity combatEntity;

        public void OnConsume(GameObject player)
        {
            combatEntity = player.GetComponent<CombatEntity>();
            combatEntity.GetPowerUp(typeOfItem, powerUpAmount); // GetPowerUp Needs parameters(String, Int)

            PlayerCharacterController controller = player.GetComponent<PlayerCharacterController>();
            controller.PowerUpDuration(powerUpAmount, powerUpDuration);
        }
    }
}
