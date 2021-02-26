using InventorySystem.Items;
using System;
using UnityEngine;

namespace ShopSystem.CraftingSystem
{
    [Serializable]
    public struct CraftingItem
    {
        //DISATTIVO IL WARNING CHE DICE CHE HANNO IL DEFAULT VALUE PERCHè NON POSSO INIZIALIZZARE NELLE STRUCT
        #pragma warning disable 0649
        [SerializeField]
        private Item item;
        public Item Item => item;

        [SerializeField]
        [Range(1, 99)]
        [Min(1)]
        private int amount;
        public int Amount => amount;
        #pragma warning restore 0649
    }
}