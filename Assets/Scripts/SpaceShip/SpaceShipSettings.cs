using Sirenix.OdinInspector;
using UnityEngine;

namespace SpaceShip {
    [CreateAssetMenu(fileName = "NewSpaceShipSettings", menuName = "New Space Ship Settings", order = 5)]
    public class SpaceShipSettings : ScriptableObject
    {
        [ListDrawerSettings(Expanded = true, ShowIndexLabels = true)]
        public Color[] levelStatusColors;

        public int MaxLevel => levelStatusColors.Length - 1;

        public Color LevelColor(int level) => levelStatusColors[level];
    }
}
