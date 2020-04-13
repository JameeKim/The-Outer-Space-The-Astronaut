using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzles.SteppingButton {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Interactable), typeof(SpriteRenderer))]
    public class SteppingButton : MonoBehaviour
    {
        private bool isActive = true;
        private readonly SteppingButtonEvent onActivated = new SteppingButtonEvent();
        private readonly SteppingButtonEvent onReset = new SteppingButtonEvent();

        private SpriteRenderer spriteRenderer;
        private AudioSource audioSource;

        public SteppingButtonSettings Settings { get; set; }
        public int SteppingOrder { get; set; }

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            audioSource = GetComponent<AudioSource>();

            spriteRenderer.color = Settings.normalTintColor;
        }

        public void Stepping(GameObject player)
        {
            if (isActive)
            {
                onActivated.Invoke(this);
            }
        }

        public void AddOnActivatedListener(UnityAction<SteppingButton> listener)
        {
            onActivated.AddListener(listener);
        }

        public void AddOnResetListener(UnityAction<SteppingButton> listener)
        {
            onReset.AddListener(listener);
        }

        public void ResetButton()
        {
            isActive = true;
            spriteRenderer.color = Settings.normalTintColor;
        }

        public void SetStatus(bool isCorrect)
        {
            isActive = false;

            if (isCorrect)
            {
                spriteRenderer.color = Settings.validTintColor;
                audioSource.PlayOneShot(Settings.correctSound);

                // feedback of the success
                Debug.Log("Checking Puzzle Answer : matched an order of the puzzle");
            }
            else
            {
                spriteRenderer.color = Settings.wrongTintColor;
                audioSource.PlayOneShot(Settings.wrongSound);

                // feedback of the failure
                Debug.Log("Checking Puzzle Answer : mismatched an order of the puzzle");

                StartCoroutine(RevertToNormal());
            }
        }

        private IEnumerator RevertToNormal()
        {
            yield return new WaitForSeconds(Settings.showWrongDuration);
            onReset.Invoke(this);
        }

        private class SteppingButtonEvent : UnityEvent<SteppingButton> {}

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
