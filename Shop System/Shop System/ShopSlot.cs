using InventorySystem;
using InventorySystem.Items;
using System;

namespace ShopSystem.ShopSystem
{
    public class ShopSlot : IInventorySlot
    {
        public ShopSlot(bool unlimited, int count)
        {
            ItemUnlimited = unlimited;
            ItemCount = unlimited ? -1 : count;
        }

        //INDICA CHE CI SONO STATI CAMBIAMENTI IN QUESTO SLOT
        public event Action<IInventorySlot, Item> OnInventorySlotChanged;

        public Item Item { get; private set; } = null;

        //RESTITUISCE IL NUMERO DI ITEM PRESENTI SULLO STACK
        public int ItemCount { get; private set; } = 0;
        public bool ItemUnlimited { get; private set; } = false;

        public bool InsertItem(Item item)
        {
            if (Item == null)
            {
                Item = item;
                OnInventorySlotChanged?.Invoke(this, Item);
                return true;
            }
            return false;
        }

        public Item RetrieveItem()
        {
            if (Item != null)
            {
                if (ItemUnlimited)
                    return Item;
                if (ItemCount > 0)
                {
                    ItemCount--;
                    OnInventorySlotChanged?.Invoke(this, Item);
                    return Item;
                }
            }
            return null;
        }
    }
}