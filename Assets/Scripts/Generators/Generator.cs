using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Generators {
    public class Generator : MonoBehaviour
    {
        [SceneObjectsOnly]
        public AudioSource unlockSoundPlayer;

        [SceneObjectsOnly]
        public GameObject puzzle;

        [SceneObjectsOnly]
        public KeyInventory inventory;

        public UnityEvent onActivated;

        private GameObject keyUI;

        private bool isUnlocked; // unlocked with the key
        private bool isActivated; // activated by the puzzle or key

        public void OnPlayerCollide(GameObject player)
        {
            if (isActivated || isUnlocked)
                return; // do nothing if this is already activated or unlocked

            if (!inventory.CanActivate(this))
                return; // do nothing if the player does not have the key

            Unlock();

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

        private void Unlock()
        {
            isUnlocked = true;

            if (keyUI != null) // clear out the key in the ui
            {
                Destroy(keyUI);
                keyUI = null;
            }

            if (unlockSoundPlayer != null)
                unlockSoundPlayer.Play();
        }
    }
}
