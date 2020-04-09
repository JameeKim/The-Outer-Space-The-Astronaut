using UnityEngine;
using UnityEngine.Events;

namespace Generators {
    public class Generator : MonoBehaviour
    {
        public UnityEvent onActivated;

        private bool isActivated;

        public void Activate()
        {
            if (isActivated)
                return;

            isActivated = true;
            onActivated.Invoke();
        }
    }
}
