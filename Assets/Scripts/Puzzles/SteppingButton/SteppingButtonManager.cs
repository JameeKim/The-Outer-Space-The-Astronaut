using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzles.SteppingButton {
    public class SteppingButtonManager : MonoBehaviour
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        [InfoBox("This is a convenience field to edit the `SteppingButtonSettings` asset directly.")]
        [FoldoutGroup("Settings")]
        [ValidateInput("MustBeSet")]
        public SteppingButtonSettings settings;

        [Space(10.0f)]
        public SteppingButton[] buttons;

        [Space(5.0f)]
        public UnityEvent onSolved;

        private int nextCorrectOrder = 0;

        private void Start()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Settings = settings;
                buttons[i].SteppingOrder = i;
                buttons[i].AddOnActivatedListener(OnButtonPressed);
                buttons[i].AddOnResetListener(OnButtonReset);
            }
        }

        private void OnButtonPressed(SteppingButton button)
        {
            if (nextCorrectOrder >= buttons.Length) return;

            bool isCorrect = button.SteppingOrder == nextCorrectOrder;
            button.SetStatus(isCorrect);
            if (isCorrect) nextCorrectOrder++;

            if (nextCorrectOrder == buttons.Length)
            {
                // rewards of solving the puzzle
                Debug.Log("Congratulation. Puzzle is solved. Go forward.");
                onSolved.Invoke();
            }
        }

        private void OnButtonReset(SteppingButton callerButton)
        {
            if (settings.resetAllWhenWrong)
            {
                nextCorrectOrder = 0;
                foreach (SteppingButton button in buttons)
                {
                    button.ResetButton();
                }
            }
            else
            {
                callerButton.ResetButton();
            }
        }

#if UNITY_EDITOR
        private bool MustBeSet(SteppingButtonSettings value, ref string errorMsg)
        {
            bool isValid = value != null;
            if (!isValid)
            {
                errorMsg = "This field must be set to a non-null value";
            }
            return isValid;
        }
#endif
    }
}
