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

        public UnityEvent onPlayerTryToRide;

        public PlayerRideEvent onPlayerRide;

        private int currentLevel;

        public int CurrentLevel => currentLevel;

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
                onPlayerTryToRide.Invoke();
            else
                onPlayerRide.Invoke(player);
        }

        [Serializable]
        public class LevelUpEvent : UnityEvent<int> {}

        [Serializable]
        public class PlayerRideEvent : UnityEvent<GameObject> {}
    }
}
