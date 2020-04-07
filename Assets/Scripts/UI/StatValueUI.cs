using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [DisallowMultipleComponent]
    public class StatValueUI : MonoBehaviour
    {
        [Title("Stat Information")]
        [Tooltip("The name of the stat shown in the UI")]
        public string statName = "Stat";

        [Title("Control Targets")]
        [Tooltip("The `Text` component of the stat name")]
        [SceneObjectsOnly]
        [ValidateInput("MustBeSet")]
        public Text nameUI;

        [Tooltip("The `Text` component of the stat value")]
        [SceneObjectsOnly]
        [ValidateInput("MustBeSet")]
        public Text valueUI;

        public void SetNewValue(int newValue, int previousValue)
        {
            valueUI.text = newValue.ToString();
        }

        private void Start()
        {
            nameUI.text = statName;
        }

#if UNITY_EDITOR
        private bool MustBeSet(Text fieldValue, ref string errorMsg)
        {
            bool isValid = fieldValue != null;
            if (!isValid)
            {
                errorMsg = "The value of this field must be set to a non-null value";
            }
            return isValid;
        }
#endif
    }
}
