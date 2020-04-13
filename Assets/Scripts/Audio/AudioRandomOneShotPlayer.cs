using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class AudioRandomOneShotPlayer : MonoBehaviour
    {
        public AudioClip[] audioClips;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayRandom()
        {
            audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
        }
    }
}
