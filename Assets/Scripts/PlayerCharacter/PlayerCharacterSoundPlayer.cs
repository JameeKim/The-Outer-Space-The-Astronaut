using Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PlayerCharacter {
    [DisallowMultipleComponent]
    public class PlayerCharacterSoundPlayer : MonoBehaviour
    {
        [SceneObjectsOnly]
        public AudioRandomOneShotPlayer combatHitSoundPlayer;

        [SceneObjectsOnly]
        public AudioRandomOneShotPlayer hurtSoundPlayer;

        public void PlayHitSound()
        {
            combatHitSoundPlayer.PlayRandom();
        }

        public void PlayHurtSound()
        {
            hurtSoundPlayer.PlayRandom();
        }
    }
}
