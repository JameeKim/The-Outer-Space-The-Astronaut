using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SelfFadeOut : MonoBehaviour
{
    public float offset = 1.0f;
    public float duration = 0.5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D maybeCollider;
    private Rigidbody2D maybeRigidBody;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maybeCollider = GetComponent<Collider2D>();
        maybeRigidBody = GetComponent<Rigidbody2D>();

        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        yield return new WaitForSeconds(offset);

        if (maybeCollider != null) Destroy(maybeCollider);
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
