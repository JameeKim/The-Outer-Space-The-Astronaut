using Combat;
using UnityEngine;

namespace Items {
    public class Caffeine : Consumable
    {
        public int enhancedValue = 1;

        protected override void Consume(GameObject player)
        {
            //Debug.Log("Player consumed a caffeine");
            CombatEntity combatEntity = player.GetComponent<CombatEntity>();

            combatEntity.ChangeCombatStat(CombatStatType.CombatPoint, enhancedValue);
        }
    }
}
