using Dialog;
using PlayerCharacter;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tutorial {
    public class TutorialBoundary : MonoBehaviour
    {
        [FoldoutGroup("Settings")]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public TutorialSettings settings;

        public DialogSeries dialogList;

        [SceneObjectsOnly]
        [HideInPrefabAssets]
        public DialogPlayer dialogPlayer;

        public void OnPlayerEnter(GameObject player)
        {
            PlayerCharacterController playerController = player.GetComponent<PlayerCharacterController>();
            Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
            Vector2 direction = rigidBody.velocity.normalized;
            playerController.DisableInputAndMoveForSeconds(settings.goBackDuration, -direction);

            dialogPlayer.PlayDialog(dialogList);
        }
    }
}
