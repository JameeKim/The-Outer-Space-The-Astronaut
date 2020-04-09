using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyHome : MonoBehaviour
    {
        [SceneObjectsOnly]
        [ListDrawerSettings(Expanded = true)]
        [Space(5.0f)]
        public Enemy[] units;

        [Title("Event Callbacks")]
        public UnityEvent onPlayerDetected;

        public UnityEvent onPlayerGetAway;

        [TitleGroup("Runtime Properties")]
        [ShowInInspector]
        [HideInEditorMode]
        [ReadOnly]
        private GameObject target;

        private bool isTargetPlayer;

        [TitleGroup("Runtime Properties")]
        [ShowInInspector]
        [HideInEditorMode]
        [ShowIf("isTargetPlayer")]
        [ListDrawerSettings(Expanded = true)]
        [ReadOnly]
        private bool[] isTargetInRange;

        private int enemyLayer;

        private GameObject Target
        {
            get => target;
            set
            {
                target = value;

                foreach (Enemy enemy in units)
                {
                    enemy.Target = value;
                }
            }
        }

        private void Start()
        {
            enemyLayer = LayerMask.NameToLayer("Enemies");

            for (int i = 0; i < units.Length; i++)
            {
                units[i].ID = i;
                units[i].AddOnPlayerEnterListener(OnPlayerEnter);
                units[i].AddOnPlayerExitListener(OnPlayerExit);
                units[i].AddOnDeathListener(OnDeath);
                onPlayerDetected.AddListener(units[i].emotes.ShowNoticed);
                onPlayerGetAway.AddListener(units[i].emotes.ShowLostInterest);
            }

            isTargetInRange = new bool[units.Length];
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (isTargetPlayer)
                return; // the target is the player, so should keep chasing them

            if (other.gameObject.layer != enemyLayer || other.isTrigger)
                return; // check if the collider is from enemies, and exclude triggers

            // Let's just assume that this is not null here, because we already checked for the `Enemies` layer.
            // Prefabs should guarantee the existence of this component.
            Enemy enemyComponent = other.GetComponent<Enemy>();
            int id = enemyComponent.ID;

            if (id >= units.Length || units[id] != enemyComponent)
                return; // this unit is not from this area, so ignore

            if (enemyComponent.Target != null)
                enemyComponent.Target = null; // arrived home, so stop
        }

        private void OnPlayerEnter(int unitId, GameObject player)
        {
            isTargetInRange[unitId] = true;

            if (isTargetPlayer)
                return; // the target is already set

            isTargetPlayer = true;
            Target = player;
            onPlayerDetected.Invoke();
        }

        private void OnPlayerExit(int unitId, GameObject player)
        {
            isTargetInRange[unitId] = false;

            if (!isTargetPlayer)
                return; // no need to do anything when the player is not being chased

            if (isTargetInRange.Any(x => x))
                return; // the player is still in range of some unit

            isTargetPlayer = false;
            Target = gameObject;
            onPlayerGetAway.Invoke();
        }

        private void OnDeath(int unitId)
        {
            onPlayerDetected.RemoveListener(units[unitId].emotes.ShowNoticed);
            onPlayerGetAway.RemoveListener(units[unitId].emotes.ShowLostInterest);

            int length = units.Length;

            if (length == 1)
            {
                Destroy(gameObject);
                return;
            }

            if (unitId >= length)
            {
                Debug.LogError($"{gameObject.name}: Received death of unit {unitId} when the length is only {length}");
                return;
            }

            units = units.RemoveAt(unitId);
            isTargetInRange = isTargetInRange.RemoveAt(unitId);
            // We do not worry about cleaning up the current target.
            // Leaving it this way can give the sense of "coming for revenge" to the player.

            for (int i = 0; i < units.Length; i++)
                units[i].ID = i; // reassign ids
        }
    }
}
