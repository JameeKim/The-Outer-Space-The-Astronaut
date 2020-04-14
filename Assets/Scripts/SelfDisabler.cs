using UnityEngine;

[DisallowMultipleComponent]
public class SelfDisabler : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
}
