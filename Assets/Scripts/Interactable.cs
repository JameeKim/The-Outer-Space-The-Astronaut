using System;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public PlayerEntered onPlayerEntered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;
        if (otherGameObject.layer == LayerMask.NameToLayer("Player"))
        {
            onPlayerEntered.Invoke(otherGameObject);
        }
    }

    [Serializable]
    public class PlayerEntered : UnityEvent<GameObject> {}
}
