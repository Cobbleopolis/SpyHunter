using UnityEngine;

namespace Cobble.Core.Lib.Items {
    
    public abstract class Item : ScriptableObject {
        
        [Tooltip("The id of the item used in the item registry.")]
        public string ItemId;
        
        [Tooltip("The display name of the item.")]
        public string Name;
        
        [Tooltip("The max stack size.")]
        public int MaxStack = 1;
        
        
        [Tooltip("The sprite used to represnt this item in an inventory slot.")]
        public Sprite ItemSprite;
        
        [Tooltip("The prefab used to represent this item in the world.")]
        public GameObject ItemPrefab;

        public abstract void UseItem(GameObject usingGameObject);

    }
}