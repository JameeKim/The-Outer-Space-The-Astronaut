using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Combat {
    [RequireComponent(typeof(Collider2D))]
    public class CombatEntity : MonoBehaviour
    {
        [ValidateInput("MustBeSet")]
        public CombatStat combatStat;

        public LayerMask attackTargetLayer;

        [TitleGroup("Event Callbacks", Order = 2)]
        [FoldoutGroup("Event Callbacks/Attack")]
        [InfoBox("void OnAttack(\n"
            + "\tCollider2D collider,\n"
            + "\tCombatEntity collisionDetector,\n"
            + "\tCombatEntity collisionTarget)")]
        public OnAttack onAttack;

        [FoldoutGroup("Event Callbacks/Health")]
        [InfoBox("void OnHealthChanged(int newHealth, int previousHealth, int maxHealth)")]
        public OnHealthChanged onHealthChanged;

        [FoldoutGroup("Event Callbacks/Health")]
        [InfoBox("void OnHealthZero(CombatEntity whatCombatEntityHasZeroHealth)")]
        public OnHealthZero onHealthZero;

        [FoldoutGroup("Event Callbacks/Stat Changes")]
        [InfoBox("void OnAttackChanged(int newAttack, int previousAttack)")]
        public OnStatChanged onAttackChanged;

        [FoldoutGroup("Event Callbacks/Stat Changes")]
        [InfoBox("void OnDefenseChanged(int newDefense, int previousDefense)")]
        public OnStatChanged onDefenseChanged;

        #region CurrentCombatStats

        [SerializeField]
        [HideInInspector]
        private int currentHealth;

        [SerializeField]
        [HideInInspector]
        private int currentAttack;

        [SerializeField]
        [HideInInspector]
        private int currentDefense;

        #endregion

        #region CombatStatProperties

        [FoldoutGroup("Combat Stats", Order = 1)]
        [ShowInInspector]
        [ReadOnly]
        [LabelText("Health")]
        public int CurrentHealth => currentHealth;

        [FoldoutGroup("Combat Stats")]
        [ShowInInspector]
        [ReadOnly]
        [LabelText("Attack")]
        public int CurrentAttack => currentAttack;

        [FoldoutGroup("Combat Stats")]
        [ShowInInspector]
        [ReadOnly]
        [LabelText("Defense")]
        public int CurrentDefense => currentDefense;

        private int InternalCurrentHealth
        {
            set
            {
                int maxHealth = combatStat.maxHealth;
                int prevHealth = currentHealth;
                currentHealth = Mathf.Max(0, Mathf.Min(maxHealth, value));

                onHealthChanged.Invoke(currentHealth, prevHealth, maxHealth);

                if (currentHealth == 0)
                    onHealthZero.Invoke(this);
            }
        }

        private int InternalCurrentAttack
        {
            set
            {
                onAttackChanged.Invoke(value, currentAttack);
                currentAttack = value;
            }
        }

        private int InternalCurrentDefense
        {
            set
            {
                onDefenseChanged.Invoke(value, currentDefense);
                currentDefense = value;
            }
        }

        #endregion

        /// <summary>
        ///   <para>Take the damage from the given attack value.</para>
        ///   <para>This computes the real damage taken and also emit all appropriate event callbacks.</para>
        /// </summary>
        /// <param name="rawDamage">The attack value of the attacker</param>
        public void GetAttacked(int rawDamage)
        {
            InternalCurrentHealth = currentHealth - (rawDamage - currentDefense);
        }

        private void Start()
        {
            currentHealth = combatStat.maxHealth;
            currentAttack = combatStat.attack;
            currentDefense = combatStat.defense;
        }

        private void OnEnable()
        {
            InternalCurrentHealth = currentHealth;
            InternalCurrentAttack = currentAttack;
            InternalCurrentDefense = currentDefense;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            GameObject otherGameObject = other.gameObject;

            if ((otherGameObject.layer & attackTargetLayer) == 0)
                return; // return if the other game object is not in the layer

            CombatEntity otherCombatEntity = otherGameObject.GetComponent<CombatEntity>();

            if (otherCombatEntity == null)
                return; // return if the other game object does not have a `CombatEntity` component

            // This method will be called on the other side (other `CombatEntity`), too.
            // Therefore, there is no need to call `GetAttacked` of the other one here.
            GetAttacked(otherCombatEntity.CurrentAttack);
            onAttack.Invoke(other, this, otherCombatEntity);
        }

        #region EventClasses

        [Serializable]
        public class OnAttack : UnityEvent<Collision2D, CombatEntity, CombatEntity> {}

        [Serializable]
        public class OnHealthChanged : UnityEvent<int, int, int> {}

        [Serializable]
        public class OnStatChanged : UnityEvent<int, int> {}

        [Serializable]
        public class OnHealthZero : UnityEvent<CombatEntity> {}

        #endregion

#if UNITY_EDITOR
        private bool MustBeSet(CombatStat value, ref string errorMsg)
        {
            bool isValid = value != null;
            if (!isValid)
            {
                errorMsg = "This field must be set to a non-null value";
            }
            return isValid;
        }
#endif
    }
}
