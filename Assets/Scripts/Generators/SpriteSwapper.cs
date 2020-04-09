using UnityEngine;

namespace Generators {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteSwapper : MonoBehaviour
    {
        public Sprite spriteToSwap;

        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SwapSprite()
        {
            spriteRenderer.sprite = spriteToSwap;
        }
    }
}
