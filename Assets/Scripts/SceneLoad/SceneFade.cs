using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SceneLoad {
    [RequireComponent(typeof(Image))]
    public class SceneFade : MonoBehaviour
    {
        public FadeType fadeType;

        public bool autoStart = true;

        public float fadeDuration = 0.5f;

        public UnityEvent onFadeFinished;

        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();

            if (autoStart)
                StartFade();
        }

        public void StartFade()
        {
            StartCoroutine(FadeCoroutine());
        }

        private IEnumerator FadeCoroutine()
        {
            Color imageColor = image.color;
            Color start = new Color(imageColor.r, imageColor.g, imageColor.b, FadeStartAlpha(fadeType));
            Color end = new Color(imageColor.r, imageColor.g, imageColor.b, FadeEndAlpha(fadeType));
            float time = 0.0f;

            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                image.color = Color.Lerp(start, end, time / fadeDuration);
                yield return null;
            }

            onFadeFinished.Invoke();
        }

        private static float FadeStartAlpha(FadeType fadeType) => fadeType == FadeType.FadeIn ? 1.0f : 0.0f;

        private static float FadeEndAlpha(FadeType fadeType) => fadeType == FadeType.FadeIn ? 0.0f : 1.0f;

        public enum FadeType
        {
            FadeIn,
            FadeOut,
        }
    }
}
