using Sirenix.OdinInspector;
using UnityEngine;

namespace SpaceShip {
    public class SpaceShipSettings : ScriptableObject
    {
        [ListDrawerSettings(Expanded = true, ShowIndexLabels = true)]
        public Color[] levelStatusColors;

        public int MaxLevel => levelStatusColors.Length - 1;

        public Color LevelColor(int level) => levelStatusColors[level];
    }
}
