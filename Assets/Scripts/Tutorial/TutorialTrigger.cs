using Dialog;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tutorial {
    public class TutorialTrigger : MonoBehaviour
    {
        [ValidateInput("DialogPlayerMustBeSet")]
        public DialogPlayer dialogPlayer;

        [FoldoutGroup("Data")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [ValidateInput("DataMustBeSet")]
        public TutorialTriggerData data;

        public void PlayTutorial()
        {
            PlayDialog();
        }

        private void PlayDialog()
        {
            data.dialogList.TutorialTrigger = this;
            dialogPlayer.PlayDialog(data.dialogList);
        }

#if UNITY_EDITOR
        private bool DialogPlayerMustBeSet(DialogPlayer value, ref string msg) => MustBeSet(value, ref msg);

        private bool DataMustBeSet(TutorialTriggerData value, ref string msg) => MustBeSet(value, ref msg);

        private static bool MustBeSet<T>(T value, ref string msg) where T : class
        {
            bool isValid = value != null;
            if (!isValid)
                msg = "The field must be set to a non-null value";
            return isValid;
        }
#endif
    }
}
