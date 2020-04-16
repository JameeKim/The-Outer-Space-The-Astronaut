using System;
using Dialog;
using UnityEngine;
using UnityEngine.Events;

namespace Tutorial {
    public class TutorialPlayer : MonoBehaviour
    {
        public GameObject tutorialGameObject;

        public UnityEvent onTutorialEnd;

        private TutorialTrigger[] tutorialTriggers;

        private bool isFinished;

        private bool IsFinished
        {
            get => isFinished;
            set
            {
                isFinished = value;
                if (isFinished)
                    onTutorialEnd.Invoke();
            }
        }

        private void Awake()
        {
            tutorialTriggers = tutorialGameObject.GetComponentsInChildren<TutorialTrigger>();

            for (int i = 1; i < tutorialTriggers.Length; i++)
            {
                SetTriggerColliderActive(tutorialTriggers[i], false);
            }
        }

        public void SkipTutorial()
        {
            if (IsFinished)
                return;

            Destroy(tutorialGameObject);
            tutorialTriggers = null;
            IsFinished = true;
        }

        public void OnDialogEnd(DialogSeries dialogList)
        {
            if (!dialogList.isTutorial)
                return;

            int index = Array.FindIndex(tutorialTriggers, t => t == dialogList.TutorialTrigger);

            if (index < 0)
                return;

            Destroy(tutorialTriggers[index].gameObject);
            tutorialTriggers = tutorialTriggers.RemoveAt(index);

            if (tutorialTriggers.Length == 0)
            {
                IsFinished = true;
                return;
            }

            SetTriggerColliderActive(tutorialTriggers[0], true);

            foreach (TutorialBoundary boundary in tutorialTriggers[0].GetComponentsInChildren<TutorialBoundary>())
            {
                boundary.SetDetectionEnabled(true);
            }
        }

        private static void SetTriggerColliderActive(TutorialTrigger trigger, bool active)
        {
            Collider2D triggerCollider = trigger.GetComponent<Collider2D>();
            if (triggerCollider != null)
                triggerCollider.enabled = active;
        }
    }
}
