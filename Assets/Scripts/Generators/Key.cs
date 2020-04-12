using Sirenix.OdinInspector;
using UnityEngine;

namespace Generators {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Key : MonoBehaviour
    {
        [SceneObjectsOnly]
        [ValidateInput("MustBeSet")]
        public Generator generator;

        public SpriteRenderer SpriteRenderer { get; private set; }

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

#if UNITY_EDITOR
        private bool MustBeSet(Generator value, ref string errorMsg)
        {
            bool isValid = value != null;
            if (!isValid)
                errorMsg = "The field must be set to a non-null value";
            return isValid;
        }
#endif
    }
}
