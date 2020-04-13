using Sirenix.OdinInspector;
using UnityEngine;

namespace Teleporters {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider2D), typeof(AudioSource))]
    public class Teleporter : MonoBehaviour
    {
        [HideInPrefabAssets]
        [SceneObjectsOnly]
        [ValidateInput("MustBeSet")]
        public Teleporter destination;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TeleportedEntity entity = other.GetComponent<TeleportedEntity>();

            if (entity == null)
                return; // only affect entities that are meant to be teleported

            if (entity.IsSent)
                return; // do not teleport this entity again if it is just sent over from another teleporter

            entity.SetSent();
            entity.RigidBody.position += (Vector2) (destination.transform.position - transform.position);
            audioSource.Play();
        }

#if UNITY_EDITOR
        private bool MustBeSet(Teleporter value, ref string errorMsg)
        {
            bool isValid = value != null;
            if (!isValid)
                errorMsg = "The destination of the teleporter must be assigned";
            return isValid;
        }
#endif
    }
}
