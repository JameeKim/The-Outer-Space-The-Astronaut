using Sirenix.OdinInspector;
using UnityEngine;

namespace Puzzles.SteppingButton {
    [CreateAssetMenu(fileName = "SteppingButtonSettings", menuName = "New Stepping Button Settings", order = 5)]
    public class SteppingButtonSettings : ScriptableObject
    {
        [Title("Answer")]
        [Tooltip("How long to show the \"wrong\" color in seconds")]
        [Min(0.1f)]
        public float showWrongDuration = 1.0f;

        [Tooltip("If the puzzle should reset all of the buttons when wrong")]
        public bool resetAllWhenWrong = true;

        [Title("Colors")]
        public Color normalTintColor = Color.white;
        public Color wrongTintColor = Color.red;
        public Color validTintColor = Color.green;

        [Title("Sounds")]
        public AudioClip correctSound;
        public AudioClip wrongSound;
        public AudioClip solvedSound;
    }
}
