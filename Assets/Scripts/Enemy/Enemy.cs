using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Enemy {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EnemyController), typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        [SceneObjectsOnly]
        public EnemyEmotes emotes;

        [AssetsOnly]
        public GameObject deadVersion;

        private EnemyController enemyController;
        private Rigidbody2D rigidBody;

        private readonly EnemyTargetEvent onPlayerEnter = new EnemyTargetEvent();
        private readonly EnemyTargetEvent onPlayerExit = new EnemyTargetEvent();
        private readonly EnemyDeathEvent onDeath = new EnemyDeathEvent();

        [Title("Runtime Properties")]
        [ShowInInspector]
        [HideInEditorMode]
        [ReadOnly]
        [LabelText("Unit ID")]
        public int ID { get; set; }

        public GameObject Target
        {
            get => enemyController.Target;
            set => enemyController.Target = value;
        }

        private void Start()
        {
            enemyController = GetComponent<EnemyController>();
            rigidBody = GetComponent<Rigidbody2D>();
        }

        public void OnPlayerEnter(GameObject player)
        {
            onPlayerEnter.Invoke(ID, player);
        }

        public void OnPlayerExit(GameObject player)
        {
            onPlayerExit.Invoke(ID, player);
        }

        public void OnDeath()
        {
            onDeath.Invoke(ID);
            GameObject newGameObject = Instantiate(deadVersion, transform.position, Quaternion.identity);
            newGameObject.GetComponent<Rigidbody2D>().velocity = rigidBody.velocity;
            Destroy(gameObject);
        }

        public void AddOnPlayerEnterListener(UnityAction<int, GameObject> listener)
        {
            onPlayerEnter.AddListener(listener);
        }

        public void AddOnPlayerExitListener(UnityAction<int, GameObject> listener)
        {
            onPlayerExit.AddListener(listener);
        }

        public void AddOnDeathListener(UnityAction<int> listener)
        {
            onDeath.AddListener(listener);
        }

        private class EnemyTargetEvent : UnityEvent<int, GameObject> {}

        private class EnemyDeathEvent : UnityEvent<int> {}
    }
}

