using Combat;
using PlayerCharacter;
using UnityEngine;

namespace Trap {
    public class PlayerDamager : MonoBehaviour
    {
        public int damage = 10;
        public float paralyzeDuration = 0.5f;

        public void DoDamage(GameObject player)
        {
            // Assume that all of these components are present because we checked if the game object is the player.
            // There is only one player character, and we rely on setting that game object correctly.
            CombatEntity combatEntity = player.GetComponent<CombatEntity>();
            PlayerCharacterAnimator animator = player.GetComponent<PlayerCharacterAnimator>();

            combatEntity.GetAttacked(damage); // do damage
            animator.GetHurt(); // play the animation to indicate the player took damage

            if (paralyzeDuration <= 0.0f)
                return; // all things done for damagers that do not paralyze the player

            // Same as above, assume all of these components are there.
            Rigidbody2D rigidBody = player.GetComponent<Rigidbody2D>();
            PlayerCharacterController controller = player.GetComponent<PlayerCharacterController>();

            rigidBody.velocity = Vector2.zero; // stop the player character
            controller.DisableInputForSeconds(paralyzeDuration); // disable input for stalling effect
        }
    }
}
