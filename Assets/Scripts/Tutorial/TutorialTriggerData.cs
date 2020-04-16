using Dialog;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tutorial {
    [CreateAssetMenu(fileName = "TutorialTriggerData", menuName = "New Tutorial Trigger Data", order = 5)]
    public class TutorialTriggerData : ScriptableObject
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public DialogSeries dialogList;
    }
}
