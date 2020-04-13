using Combat;
using UnityEngine;

namespace Items {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable))]
    public class Caffeine : MonoBehaviour
    {
        public int enhancedValue = 1;
        public void OnConsume(GameObject player)
        {
            //Debug.Log("Player consumed a caffeine");
            CombatEntity combatEntity = player.GetComponent<CombatEntity>();

            combatEntity.ChangeCombatStat(CombatStatType.CombatPoint, enhancedValue);
        }
    }
}
