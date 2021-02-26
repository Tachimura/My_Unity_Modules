using InventorySystem.Items;
using System;
using UnityEngine;

namespace ShopSystem.ShopSystem
{
    [Serializable]
    public class ShopItem
    {
        [SerializeField]
        private Item item = null;
        public Item Item => item;

        [SerializeField]
        private bool itemUnlimited = false;
        public bool ItemUnlimited => itemUnlimited;

        [SerializeField]
        private int itemCount = 1;
        public int ItemCount => itemCount;
    }
}