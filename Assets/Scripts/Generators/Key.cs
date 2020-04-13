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

        public Sprite Sprite { get; private set; }

        private void Awake()
        {
            Sprite = GetComponent<SpriteRenderer>().sprite;
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
