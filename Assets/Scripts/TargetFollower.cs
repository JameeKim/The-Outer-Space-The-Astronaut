using Sirenix.OdinInspector;
using UnityEngine;

[DisallowMultipleComponent]
public class TargetFollower : MonoBehaviour
{
    [Title("Target")]
    [HideLabel]
    [Tooltip("The `Transform` component of the target to follow. The value can be set to null.")]
    public Transform target;

    [Title("Smooth Movement")]
    [Tooltip("Whether to enable smooth/damped movement")]
    public bool smooth = true;

    [ShowIf("smooth")]
    [MinValue(0.1f)]
    [Tooltip("How much time it takes for this game object to reach the target point")]
    public float smoothTime = 0.3f;

    // The `Transform` component of this game object is cached in order to prevent performance drops
    private Transform transformCache;

    // This is used and stored by `SmoothDamp` static method of `Vector3` for smooth following behavior
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        // cache the component
        transformCache = transform;
    }

    // The position update should take place in the `FixedUpdate` loop because... I don't know, maybe because the player
    // character moves in the `FixedUpdate` loop? Neither `Update` nor `LateUpdate` work properly, causing a weird
    // jitter.
    private void FixedUpdate()
    {
        // do nothing if the target is not set
        if (!target)
            return;

        // this object should only follow the x and y positions of the target, so the z value is dropped by casting the
        // returned `Vector3` into `Vector2`
        Vector2 targetPosition2D = target.position;

        // this is the real target position, having the same z value
        Vector3 targetPosition = new Vector3(targetPosition2D.x, targetPosition2D.y, transformCache.position.z);

        if (smooth)
        {
            transformCache.position = Vector3.SmoothDamp(
                transformCache.position,
                targetPosition,
                ref velocity,
                smoothTime);
        }
        else
        {
            transformCache.position = targetPosition;
        }
    }
}
