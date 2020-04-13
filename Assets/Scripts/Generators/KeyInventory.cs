using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Generators {
    public class KeyInventory : MonoBehaviour
    {
        [SceneObjectsOnly]
        public Transform keyInventoryUI;

        [AssetsOnly]
        public GameObject keyUIPrefab;

        [ShowInInspector]
        [HideInEditorMode]
        [ListDrawerSettings(Expanded = true)]
        [ReadOnly]
        private readonly List<Generator> unlockedGenerators = new List<Generator>();

        public void AddKey(Key newKey)
        {
            if (unlockedGenerators.Exists(g => g == newKey.generator))
                return;

            unlockedGenerators.Add(newKey.generator);
            GameObject ui = Instantiate(keyUIPrefab, keyInventoryUI, false);
            ui.GetComponent<Image>().sprite = newKey.SpriteRenderer.sprite;
            newKey.generator.SetKeyUI(ui);
        }

        public bool CanActivate(Generator generator)
        {
            return unlockedGenerators.Exists(g => g == generator);
        }
    }
}
