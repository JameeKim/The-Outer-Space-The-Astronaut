using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Dialog {
    [Serializable]
    public struct DialogItem
    {
        public bool defaultName;

        [HideIf("defaultName")]
        public string speakerName;

        [TextArea]
        public string content;
    }
}

