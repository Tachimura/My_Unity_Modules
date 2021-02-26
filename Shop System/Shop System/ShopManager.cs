using InventorySystem;
using InventorySystem.Items;
using UnityEngine;

namespace ShopSystem.ShopSystem
{
    [CreateAssetMenu(fileName = "Shop Manager - ", menuName = "Shop System/Shop System/Shop Manager")]
    public class ShopManager : ScriptableObject, IShop
    {
        [SerializeField]
        private ShopItem[] shopItems = null;

        [SerializeField]
        [Range(0,200)]
        private int shopPrice = 100;
        public int ShopPrice => shopPrice;

        public int ShopSize => shopItems.Length;

        private ShopSlot[] slots = null;

        public Item AcquireItem(IInventory inventory, int position)
        {
            Item item = RetrieveItem(position);
            if(item != null)
                inventory.InsertItem(Instantiate(item));
            return item;
        }

        private void Awake() => InitShopManager();

        private void InitShopManager()
        {
            lock (this)
            {
                slots = new ShopSlot[ShopSize];
                for (int position = 0; position < ShopSize; position++)
                {
                    ShopItem item = shopItems[position];
                    slots[position] = new ShopSlot(item.ItemUnlimited, item.ItemCount);
                    slots[position].InsertItem(item.Item);
                }
            }
        }

        public IInventorySlot InventorySlotAtPosition(int position)
        {
            if(slots == null)
                InitShopManager();
            return (position >= 0 && position < ShopSize) ? slots[position] : null;
        }

        private Item RetrieveItem(int position)
        {
            //SE RICHIEDO UN ITEM NELL'INVENTARIO
            if (position >= 0 && position < ShopSize)
                //RITORNO L'OGGETTO NELLA POSIZIONE RICHIESTA
                return slots[position].RetrieveItem();
            //ALTRIMENTI RITORNO NULL
            return null;
        }

        public int RetrieveItemCount(Item item)
        {
            int itemCount = 0;
            //CERCO IN OGNI SLOT
            foreach (IInventorySlot slot in slots)
            {
                //SE HO UN OGGETTO, ED è QUELLO CHE CERCO MI PRENDO IL NUMERO RIMANENTE
                if (slot.Item != null && slot.Item.EqualsTo(item))
                {
                    //INFINITO
                    if (slot.ItemCount == -1)
                        return -1;
                    itemCount += slot.ItemCount;
                }
            }
            return itemCount;
        }
    }
}