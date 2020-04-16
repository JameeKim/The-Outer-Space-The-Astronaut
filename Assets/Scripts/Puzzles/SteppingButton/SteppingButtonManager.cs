using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzles.SteppingButton {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
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

        private AudioSource audioSource;

        private int nextCorrectOrder = 0;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

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
            if (nextCorrectOrder >= buttons.Length)
                return;

            bool isCorrect = button.SteppingOrder == nextCorrectOrder;

            if (!isCorrect && settings.resetAllWhenWrong)
            {
                foreach (SteppingButton eachButton in buttons)
                {
                    eachButton.SetStatus(false);
                }
            }
            else
            {
                button.SetStatus(isCorrect);
            }

            audioSource.PlayOneShot(isCorrect ? settings.correctSound : settings.wrongSound);

            if (!isCorrect)
                return;

            nextCorrectOrder++;

            if (nextCorrectOrder < buttons.Length)
                return;

            audioSource.PlayOneShot(settings.solvedSound);
            onSolved.Invoke();
        }

        private void OnButtonReset(SteppingButton button)
        {
            if (settings.resetAllWhenWrong)
                nextCorrectOrder = 0;

            button.ResetButton();
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
