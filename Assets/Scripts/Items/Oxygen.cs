using Combat;
using UnityEngine;

namespace Items {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable))]
    public class Oxygen : MonoBehaviour
    {
        public int healingValue = 10;

        public void OnConsume(GameObject player)
        {
            //Debug.Log("Player consumed an oxygen crystal");
            CombatEntity combatEntity = player.GetComponent<CombatEntity>();

            combatEntity.ChangeCombatStat(CombatStatType.HealthPoint, healingValue);
        }
    }
}
