using Sirenix.OdinInspector;
using UnityEngine;

namespace SpaceShip {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpaceShipStatus : MonoBehaviour
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [FoldoutGroup("Settings")]
        public SpaceShipSettings settings;

        private SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = settings.LevelColor(0);
        }

        public void SetLevel(int currentLevel)
        {
            spriteRenderer.color = settings.LevelColor(currentLevel);
        }
    }
}
