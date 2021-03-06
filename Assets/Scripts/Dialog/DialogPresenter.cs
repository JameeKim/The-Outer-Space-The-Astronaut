﻿using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dialog {
    [DisallowMultipleComponent]
    public class DialogPresenter : MonoBehaviour
    {
        [Title("Properties")]
        public string nextString = "Next";

        public string endString = "Quit";

        public DialogEvent onDialogStart;

        public DialogEvent onDialogEnd;

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
        [ValidateInput("ButtonMustBeSet")]
        public Button nextButton;

        [SceneObjectsOnly]
        [ValidateInput("TextMustBeSet")]
        public Text nextButtonText;

        [SceneObjectsOnly]
        [ValidateInput("GameObjectMustBeSet")]
        public GameObject skipTutorial;

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
            skipTutorial.SetActive(dialogList.isTutorial);
            onDialogStart.Invoke(dialogList);
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
            SetButtonFocus(index != 0);
        }

        public void FinishDialog()
        {
            onDialogEnd.Invoke(dialogList);
            dialogList = null;
        }

        private void SetPreviousButtonActive(bool active)
        {
            previousButton.enabled = active;
            previousButtonImage.enabled = active;
            previousButtonText.enabled = active;
        }

        private void SetButtonFocus(bool isPreviousButtonActive)
        {
            if (!isPreviousButtonActive)
                EventSystem.current.SetSelectedGameObject(nextButton.gameObject);
        }

        [Serializable]
        public class DialogEvent : UnityEvent<DialogSeries> {}

#if UNITY_EDITOR
        private bool TextMustBeSet(Text value, ref string msg) => MustBeSet(value, ref msg);
        private bool ButtonMustBeSet(Button value, ref string msg) => MustBeSet(value, ref msg);
        private bool ImageMustBeSet(Image value, ref string msg) => MustBeSet(value, ref msg);
        private bool GameObjectMustBeSet(GameObject value, ref string msg) => MustBeSet(value, ref msg);

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
