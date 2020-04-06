using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialog {
    [CreateAssetMenu(fileName = "NewDialogList", menuName = "New Dialog List", order = 5)]
    public class DialogSeries : ScriptableObject
    {
        [ListDrawerSettings(Expanded = true)]
        public DialogItem[] dialogItems;
    }
}
