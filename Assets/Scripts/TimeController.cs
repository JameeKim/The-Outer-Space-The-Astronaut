using UnityEngine;

[DisallowMultipleComponent]
public class TimeController : MonoBehaviour
{
    public float slowTimeScale = 0.2f;

    public void SetTimeSlow()
    {
        Time.timeScale = slowTimeScale;
    }

    public void SetTimeStop()
    {
        Time.timeScale = 0.0f;
    }

    public void SetTimeNormal()
    {
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        SetTimeNormal();
    }
}
