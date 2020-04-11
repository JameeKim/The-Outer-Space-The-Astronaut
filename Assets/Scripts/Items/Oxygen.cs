using Combat;
using UnityEngine;


namespace Items {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable))]
    public class Oxygen : MonoBehaviour
    {
        public int powerUpAmount = 10;
        string typeOfItem = "Oxygen";

        private CombatEntity combatEntity;

        public void OnConsume(GameObject player)
        {
            combatEntity = player.GetComponent<CombatEntity>();
            combatEntity.GetPowerUp(typeOfItem, powerUpAmount); // GetPowerUp Needs parameters(String, Int)
        }
    }
}
