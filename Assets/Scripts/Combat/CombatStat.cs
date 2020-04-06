using UnityEngine;

namespace Combat {
    [CreateAssetMenu(fileName = "NewCombatStat", menuName = "New Combat Stat", order = 5)]
    public class CombatStat : ScriptableObject
    {
        [Min(10)]
        public int maxHealth = 20;

        [Min(1)]
        public int attack = 1;

        [Min(0)]
        public int defense = 0;
    }
}
