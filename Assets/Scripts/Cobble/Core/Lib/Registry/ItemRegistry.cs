using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cobble.Core.Lib.Items;

namespace Cobble.Core.Lib.Registry {
    public static class ItemRegistry {
        
        private static readonly Dictionary<string, Item> ItemDictionary = new Dictionary<string, 
            Item>();

        public static void RegisterItem(Item item) {
            if (string.IsNullOrEmpty(item.ItemId))
                throw new ArgumentException("Error while registering item: The item's id can not be null or empty");
            if(ItemDictionary.ContainsKey(item.ItemId))
                throw new ArgumentException("Error while registering item: The item id \"" + item.ItemId + "\" already exists in the item registry.");
            
            ItemDictionary.Add(item.ItemId, item);
        }

        public static Item GetItem(string key) {
            return ItemDictionary[key];
        }

        public static Item[] GetAllItems() {
            return ItemDictionary.Values.ToArray();
        }

        public static void RegisterItems() {
            foreach (var itemSo in Resources.LoadAll<Item>("Items"))
                RegisterItem(itemSo);
        }
    }
}