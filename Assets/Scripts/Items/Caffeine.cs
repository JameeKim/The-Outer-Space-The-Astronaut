using UnityEngine;

namespace Items {
    [RequireComponent(typeof(Interactable))]
    public class Caffeine : MonoBehaviour
    {
        public void OnConsume(GameObject player)
        {
            Debug.Log("Player consumed a caffeine");
            // TODO
        }
    }
}
