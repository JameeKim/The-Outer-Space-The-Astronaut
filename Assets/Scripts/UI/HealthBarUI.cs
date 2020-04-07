using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HealthBarUI : MonoBehaviour
    {
        [SceneObjectsOnly]
        [ValidateInput("RectTransformMustBeSet")]
        public RectTransform innerBar;

        [SceneObjectsOnly]
        [ValidateInput("TextMustBeSet")]
        public Text valueText;

        public void SetHealth(int current, int previous, int max)
        {
            SetInnerBar((float)Mathf.Min(current, max) / max);
            valueText.text = $"{current} / {max}";
        }

        private void SetInnerBar(float proportion)
        {
            innerBar.anchorMax = new Vector2(proportion, innerBar.anchorMax.y);
        }

#if UNITY_EDITOR
        private bool RectTransformMustBeSet(RectTransform value, ref string errorMsg) => MustBeSet(value, ref errorMsg);

        private bool TextMustBeSet(Text value, ref string errorMsg) => MustBeSet(value, ref errorMsg);

        private static bool MustBeSet<T>(T value, ref string errorMsg) where T : class
        {
            bool isValid = value != null;
            if (!isValid)
                errorMsg = "This field must be set to a non-null value";
            return isValid;
        }
#endif
    }
}
