using Combat;
using UnityEngine;

namespace Items {
    public class Oxygen : Consumable
    {
        public int healingValue = 10;

        protected override void Consume(GameObject player)
        {
            //Debug.Log("Player consumed an oxygen crystal");
            CombatEntity combatEntity = player.GetComponent<CombatEntity>();

            combatEntity.ChangeCombatStat(CombatStatType.HealthPoint, healingValue);
        }
    }
}
