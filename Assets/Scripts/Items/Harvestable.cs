using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Items {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Harvestable : MonoBehaviour
    {
        // TODO: also disable the collider when inactive

        [Title("Status")]
        [Tooltip("If this is currently able to be harvested or not")]
        public bool isActive = true;

        [SerializeField]
        [Min(0.0f)]
        [PropertyTooltip("Currently left time before regeneration")]
        [HideIf("isActive")]
        private float timeLeftUntilRegen;

        [Title("Properties")]
        [Min(0.0f)]
        [Tooltip("How many seconds it needs to be regenerated")]
        public float spawnTime = 60.0f;

        [Tooltip("Callbacks to call when this is harvested")]
        public OnHarvest onHarvest;

        [Tooltip("Callbacks to call when this is regenerated")]
        public OnRegenerate onRegenerate;

        private SpriteRenderer spriteRenderer;
        private Sprite sprite;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            sprite = spriteRenderer.sprite; // save the sprite
        }

        private void Update()
        {
            if (timeLeftUntilRegen <= 0.0f) return;

            timeLeftUntilRegen -= Time.deltaTime;

            if (timeLeftUntilRegen <= 0.0f) // time to regenerate
            {
                isActive = true;
                spriteRenderer.sprite = sprite; // put back the sprite
                onRegenerate.Invoke(); // signal the regeneration
            }
        }

        public void Harvest(GameObject player)
        {
            if (!isActive) return; // failed to harvest as it is not active

            isActive = false; // set this inactive to prevent harvesting
            spriteRenderer.sprite = null; // get rid of the sprite to hide from the user
            timeLeftUntilRegen = spawnTime; // set countdown

            // signal success
            onHarvest.Invoke(player);
        }

        [Serializable]
        public class OnHarvest : UnityEvent<GameObject> {}

        [Serializable]
        public class OnRegenerate : UnityEvent {}
    }
}
