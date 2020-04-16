using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class EnableDisableDetector : MonoBehaviour
{
    public bool skipFirstTime;

    public UnityEvent onEnable;

    public UnityEvent onDisable;

    private bool enabledOnce;

    private bool disabledOnce;

    private void OnEnable()
    {
        if (skipFirstTime && !enabledOnce)
        {
            enabledOnce = true;
            return;
        }

        onEnable.Invoke();
    }

    private void OnDisable()
    {
        if (skipFirstTime && !disabledOnce)
        {
            disabledOnce = true;
            return;
        }

        onDisable.Invoke();
    }
}
