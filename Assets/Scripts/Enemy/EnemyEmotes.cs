using System.Collections;
using UnityEngine;

namespace Enemy {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyEmotes : MonoBehaviour
    {
        [Tooltip("How long to show the emote")]
        public float showDuration = 1.0f;

        [Tooltip("Emote to show when detected a player")]
        public Sprite noticed;

        [Tooltip("Emote to show when the player got out of range")]
        public Sprite lostInterest;

        private SpriteRenderer spriteRenderer;
        private WaitForSeconds waitForSeconds;

        private Coroutine currentCoroutine;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            waitForSeconds = new WaitForSeconds(showDuration);
        }

        public void ShowNoticed()
        {
            CheckAndStopThenStartCoroutine(ShowEmote(noticed));
        }

        public void ShowLostInterest()
        {
            CheckAndStopThenStartCoroutine(ShowEmote(lostInterest));
        }

        private void CheckAndStopThenStartCoroutine(IEnumerator routine)
        {
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            currentCoroutine = StartCoroutine(routine);
        }

        private IEnumerator ShowEmote(Sprite emote)
        {
            spriteRenderer.sprite = emote;
            yield return waitForSeconds;
            // ReSharper disable once Unity.InefficientPropertyAccess
            spriteRenderer.sprite = null;
            currentCoroutine = null;
        }
    }
}
