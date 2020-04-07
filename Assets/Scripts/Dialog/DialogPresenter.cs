using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Dialog {
    [DisallowMultipleComponent]
    public class DialogPresenter : MonoBehaviour
    {
        [Title("Properties")]
        public string nextString = "Next";

        public string endString = "Quit";

        public UnityEvent onDialogStart;

        public UnityEvent onDialogEnd;

        [Title("Control Targets")]
        [SceneObjectsOnly]
        [ValidateInput("TextMustBeSet")]
        public Text speakerNameText;

        [SceneObjectsOnly]
        [ValidateInput("TextMustBeSet")]
        public Text dialogContentText;

        [SceneObjectsOnly]
        [ValidateInput("ButtonMustBeSet")]
        public Button previousButton;

        [SceneObjectsOnly]
        [ValidateInput("ImageMustBeSet")]
        public Image previousButtonImage;

        [SceneObjectsOnly]
        [ValidateInput("TextMustBeSet")]
        public Text previousButtonText;

        [SceneObjectsOnly]
        [ValidateInput("TextMustBeSet")]
        public Text nextButtonText;

        private DialogSeries dialogList;
        private int dialogLength;
        private int index;

        public void StartDialog(DialogSeries dialogSeries)
        {
            if (dialogList)
            {
                Debug.LogError("Dialog start requested, but already showing another one");
                return;
            }

            dialogList = dialogSeries;
            dialogLength = dialogList.dialogItems.Length;
            index = 0;
            onDialogStart.Invoke();
            ShowDialog("StartDialog");
        }

        public void ShowNext()
        {
            index++;
            ShowDialog("ShowNext");
        }

        public void ShowPrevious()
        {
            index--;
            ShowDialog("ShowPrevious");
        }

        private void ShowDialog(string methodName)
        {
            if (!dialogList) // error and exit if the dialog list is not set
            {
                Debug.LogError($"Dialog `{methodName} called when there is not a dialog list to show");
                FinishDialog();
                return;
            }

            if (index < 0) // error and exit if trying to show dialog at negative index
            {
                Debug.LogError($"Dialog `{methodName}` called when the index is below 0");
                FinishDialog();
                return;
            }

            if (index >= dialogLength) // finish the dialog when trying to show dialog pass the list
            {
                FinishDialog();
                return;
            }

            DialogItem dialogItem = dialogList.dialogItems[index];
            speakerNameText.text = dialogItem.defaultName ? "Player" : dialogItem.speakerName; // TODO default speaker name
            dialogContentText.text = dialogItem.content; // set content
            nextButtonText.text = index == dialogLength - 1 ? endString : nextString; // set text of next button
            SetPreviousButtonActive(index != 0); // show or hide previous button
        }

        private void FinishDialog()
        {
            dialogList = null;
            onDialogEnd.Invoke();
        }

        private void SetPreviousButtonActive(bool active)
        {
            previousButton.enabled = active;
            previousButtonImage.enabled = active;
            previousButtonText.enabled = active;
        }

#if UNITY_EDITOR
        private bool TextMustBeSet(Text variable, ref string errorMsg)
        {
            bool isValid = variable != null;

            if (!isValid)
            {
                errorMsg = "The field must be set to a non-null value";
            }

            return isValid;
        }

        private bool ButtonMustBeSet(Button variable, ref string errorMsg)
        {
            bool isValid = variable != null;

            if (!isValid)
            {
                errorMsg = "The field must be set to a non-null value";
            }

            return isValid;
        }

        private bool ImageMustBeSet(Image variable, ref string errorMsg)
        {
            bool isValid = variable != null;

            if (!isValid)
            {
                errorMsg = "The field must be set to a non-null value";
            }

            return isValid;
        }
#endif
    }
}
