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
            tutorialTriggers.RemoveAt(index);

            if (tutorialTriggers.Length == 0)
                IsFinished = true;
        }
    }
}
