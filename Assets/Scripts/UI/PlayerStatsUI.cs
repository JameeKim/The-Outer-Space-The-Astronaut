using Sirenix.OdinInspector;
using UnityEngine;

namespace UI {
    public class PlayerStatsUI : MonoBehaviour
    {
        [SceneObjectsOnly]
        [ValidateInput("HealthBarMustBeSet")]
        public HealthBarUI healthBar;

        [SceneObjectsOnly]
        [ValidateInput("StatUIMustBeSet")]
        public StatValueUI attackStat;

        [SceneObjectsOnly]
        [ValidateInput("StatUIMustBeSet")]
        public StatValueUI defenseStat;

        public void SetHealth(int current, int previous, int max)
        {
            healthBar.SetHealth(current, previous, max);
        }

        public void SetAttack(int current, int previous)
        {
            attackStat.SetNewValue(current, previous);
        }

        public void SetDefense(int current, int previous)
        {
            defenseStat.SetNewValue(current, previous);
        }

#if UNITY_EDITOR
        private bool HealthBarMustBeSet(HealthBarUI value, ref string msg) => MustBeSet(value, ref msg);

        private bool StatUIMustBeSet(StatValueUI value, ref string msg) => MustBeSet(value, ref msg);

        private static bool MustBeSet<T>(T value, ref string errorMsg) where T : class
        {
            bool isValid = value != null;
            if (!isValid)
                errorMsg = "This field must be set to a non-null value";
            return isValid;
        }
#endif
    }
}
