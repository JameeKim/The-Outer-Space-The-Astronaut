using System;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class Interactable : MonoBehaviour
{
    public AudioClip interactSound;

    public PlayerEntered onPlayerEntered;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherGameObject = other.gameObject;

        if (otherGameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        if (interactSound != null)
            audioSource.PlayOneShot(interactSound);

        onPlayerEntered.Invoke(otherGameObject);
    }

    [Serializable]
    public class PlayerEntered : UnityEvent<GameObject> {}
}
