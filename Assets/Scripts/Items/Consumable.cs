using PlayerCharacter;
using UnityEngine;

namespace Items {
    [DisallowMultipleComponent]
    public abstract class Consumable : MonoBehaviour
    {
        public void OnConsume(GameObject player)
        {
            Consume(player);
            player.GetComponent<ItemConsumer>().Consume(this);
        }

        protected abstract void Consume(GameObject player);
    }
}
