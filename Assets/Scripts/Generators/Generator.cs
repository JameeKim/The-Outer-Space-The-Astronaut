using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Generators {
    public class Generator : MonoBehaviour
    {
        [SceneObjectsOnly]
        public GameObject puzzle;

        [SceneObjectsOnly]
        public KeyInventory inventory;

        public UnityEvent onActivated;

        private GameObject keyUI;
        private bool isActivated;

        public void OnPlayerCollide(GameObject player)
        {
            if (isActivated)
                return; // do nothing if this is already activated

            if (!inventory.CanActivate(this))
                return; // do nothing if the player does not have the key

            if (keyUI != null) // clear out the key in the ui
            {
                Destroy(keyUI);
                keyUI = null;
            }

            if (puzzle == null)
                Activate(); // activate if it does not have a puzzle associated with it
            else
                puzzle.SetActive(true); // make the puzzle appear if it does have an associated puzzle
        }

        public void Activate()
        {
            if (isActivated)
                return;

            isActivated = true;
            onActivated.Invoke();
        }

        public void SetKeyUI(GameObject ui)
        {
            keyUI = ui;
        }
    }
}
