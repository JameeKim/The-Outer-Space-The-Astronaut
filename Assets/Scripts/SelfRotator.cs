using UnityEngine;

[DisallowMultipleComponent]
public class SelfRotator : MonoBehaviour
{
    public float speed = 1.0f;

    private Transform transformCache;

    private void Awake()
    {
        transformCache = transform;
    }

    private void Update()
    {
        transformCache.rotation *= Quaternion.AngleAxis(speed * Time.deltaTime, Vector3.forward);
    }
}
