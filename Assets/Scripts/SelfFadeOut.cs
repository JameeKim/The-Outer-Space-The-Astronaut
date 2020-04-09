using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SelfFadeOut : MonoBehaviour
{
    [Tooltip("If fade out should start automatically")]
    public bool autoStart = true;

    [Tooltip("Delay from trigger to the start of fade out")]
    [Min(0.0f)]
    public float offset = 1.0f;

    [Tooltip("Duration of the fade out")]
    [Min(0.01f)]
    public float duration = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D maybeCollider;
    private Rigidbody2D maybeRigidBody;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maybeCollider = GetComponent<Collider2D>();
        maybeRigidBody = GetComponent<Rigidbody2D>();

        if (autoStart)
            StartCoroutine(FadeOutCoroutine());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        if (offset > 0.0f)
            yield return new WaitForSeconds(offset);

        if (maybeCollider != null) maybeCollider.enabled = false;
        if (maybeRigidBody != null) Destroy(maybeRigidBody);

        float time = 0.0f;
        Color start = spriteRenderer.color;
        Color end = Color.clear;

        while (time < duration)
        {
            time += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(start, end, time / duration);
            yield return null;
        }

        Destroy(gameObject);
    }
}
