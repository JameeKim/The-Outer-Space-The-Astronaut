using Sirenix.OdinInspector;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    [SceneObjectsOnly]
    public GameObject gameObjectToEnable;

    public bool autoEnable = true;

    private void Start()
    {
        if (autoEnable)
            Enable();
    }

    public void Enable()
    {
        gameObjectToEnable.SetActive(true);
    }
}
