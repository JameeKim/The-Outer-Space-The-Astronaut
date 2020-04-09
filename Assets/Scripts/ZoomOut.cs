using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class ZoomOut : MonoBehaviour
{
    [Min(0.1f)]
    public float zoomOutDuration = 1.0f;

    [Min(3.0f)]
    public float endSize = 5.0f;

    private Camera cameraComponent;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
    }

    public void StartZoomOut()
    {
        StartCoroutine(ZoomOutCoroutine());
    }

    private IEnumerator ZoomOutCoroutine()
    {
        float start = cameraComponent.orthographicSize;
        float end = endSize;
        float time = 0.0f;

        while (time < zoomOutDuration)
        {
            time += Time.deltaTime;
            cameraComponent.orthographicSize = Mathf.SmoothStep(start, end, time / zoomOutDuration);
            yield return null;
        }
    }
}
