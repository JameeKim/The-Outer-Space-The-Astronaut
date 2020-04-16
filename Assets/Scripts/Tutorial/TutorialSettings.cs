using UnityEngine;

namespace Tutorial {
    public class TutorialSettings : ScriptableObject
    {
        [Min(0.01f)]
        public float goBackDuration = 0.5f;
    }
}
