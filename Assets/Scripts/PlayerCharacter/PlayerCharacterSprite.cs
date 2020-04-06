using Sirenix.OdinInspector;
using UnityEngine;

namespace PlayerCharacter {
    public class PlayerCharacterSprite : MonoBehaviour
    {
        [ValidateInput("MustBeSet")]
        public SpriteRenderer spriteRenderer;

        public Sprite CurrentSprite
        {
            get => spriteRenderer.sprite;
            set => spriteRenderer.sprite = value;
        }

#if UNITY_EDITOR
        private bool MustBeSet(SpriteRenderer value, ref string errorMsg)
        {
            bool isValid = value != null;
            if (!isValid)
            {
                errorMsg = "The field must be set to a non-null value";
            }
            return isValid;
        }
#endif
    }
}
