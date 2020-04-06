using UnityEngine;

public class SelfDisabler : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
