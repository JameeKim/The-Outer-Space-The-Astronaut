using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
