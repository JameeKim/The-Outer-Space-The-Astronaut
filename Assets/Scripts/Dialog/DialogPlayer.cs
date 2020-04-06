﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialog {
    public class DialogPlayer : MonoBehaviour
    {
        [InfoBox("This component is meant to be used as a way to register a callback to play a dialog. "
            + "Expose a `UnityEvent` public field on the desired component, and register the `PlayDialog` method "
            + "of this component with the wanted dialog list asset as the argument. "
            + "Then, invoke that `UnityEvent` in the code of the component in the desired places.")]
        [SceneObjectsOnly]
        [ValidateInput("MustBeSet")]
        public DialogPresenter dialogPresenter;

        public void PlayDialog(DialogSeries dialogList)
        {
            dialogPresenter.gameObject.SetActive(true);
            dialogPresenter.StartDialog(dialogList);
        }

#if UNITY_EDITOR
        private bool MustBeSet(DialogPresenter value, ref string errorMsg)
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
