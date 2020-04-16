using System;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerCharacter {
    public class ItemConsumer : MonoBehaviour
    {
        public UnityEvent onFirstOxygen;

        public UnityEvent onFirstCaffeine;

        private Type[] consumedItems = {};

        public void Consume(Consumable item, bool shouldNotify = true)
        {
            Consume(item.GetType(), shouldNotify);
        }

        public void Consume(Type itemType, bool shouldNotify = true)
        {
            if (Array.Exists(consumedItems, t => t == itemType))
                return;

            consumedItems = consumedItems.Add(itemType);
            if (shouldNotify)
                InvokeCallbacks(itemType);
        }

        private void InvokeCallbacks(Type itemType)
        {
            if (itemType == typeof(Oxygen))
                onFirstOxygen.Invoke();
            else if (itemType == typeof(Caffeine))
                onFirstCaffeine.Invoke();
        }
    }
}
