using UnityEngine;

[DisallowMultipleComponent]
public class SelfDisabler : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
