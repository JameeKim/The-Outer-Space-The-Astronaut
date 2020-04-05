using UnityEngine;

namespace Items {
    [RequireComponent(typeof(Interactable))]
    public class Oxygen : MonoBehaviour
    {
        public void OnConsume(GameObject player)
        {
            Debug.Log("Player consumed an oxygen crystal");
            // TODO
        }
    }
}
