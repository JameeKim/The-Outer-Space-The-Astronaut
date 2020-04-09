using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShip {
    public class SpaceShipLevel : MonoBehaviour
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [FoldoutGroup("Settings")]
        public SpaceShipSettings settings;

        public LevelUpEvent onLevelUp;

        private int currentLevel;

        public int CurrentLevel => currentLevel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                LevelUp();
        }

        public void LevelUp()
        {
            if (currentLevel == settings.MaxLevel)
            {
                Debug.LogError("Tried to level up when the current level is at its maximum");
                return;
            }

            currentLevel++;
            onLevelUp.Invoke(currentLevel);
        }

        public void OnPlayerCollide(GameObject player)
        {
            if (currentLevel < settings.MaxLevel)
                return;

            Debug.Log("The game should end here"); // TODO play game finished scene
        }

        [Serializable]
        public class LevelUpEvent : UnityEvent<int> {}
    }
}
