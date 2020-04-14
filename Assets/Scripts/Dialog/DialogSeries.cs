using Sirenix.OdinInspector;
using Tutorial;
using UnityEngine;

namespace Dialog {
    [CreateAssetMenu(fileName = "NewDialogList", menuName = "New Dialog List", order = 5)]
    public class DialogSeries : ScriptableObject
    {
        public bool isTutorial;

        [ListDrawerSettings(Expanded = true)]
        public DialogItem[] dialogItems;

        public TutorialTrigger TutorialTrigger { get; set; }
    }
}
