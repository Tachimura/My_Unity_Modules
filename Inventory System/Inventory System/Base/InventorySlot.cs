using InventorySystem.Items;
using System;
using System.Collections.Generic;

namespace InventorySystem
{
    [Serializable]
    public class InventorySlot : IInventorySlot
    {
        //INDICA CHE CI SONO STATI CAMBIAMENTI IN QUESTO SLOT
        public event Action<IInventorySlot, Item> OnInventorySlotChanged;
        public Item Item
        {
            get
            {
                //SE CI SONO DEGLI OGGETTI, RITORNO IL PRIMO
                if (ItemCount > 0)
                    return items.Peek();
                //ALTRIMENTI RITORNO NULL
                else return null;
            }
        }
        //RESTITUISCE IL NUMERO DI ITEM PRESENTI SULLO STACK
        public int ItemCount => items.Count;

        private readonly Stack<Item> items = new Stack<Item>();

        public virtual bool InsertItem(Item item)
        {
            bool canInsert = false;
            //SE LO SLOT è VUOTO ALLORA LO RIEMPO
            if (Item == null)
                canInsert = true;
            //ALTRIMENTI, SE LO SLOT NON è VUOTO
            //SE CONTIENE UN OGGETTO UGUALE E POSSO STACKARLO, LO STACKO
            else if (Item.EqualsTo(item) && item.IsStackable)
                canInsert = true;
            //SE POSSO INSERIRE L'ITEM LO INSERISCO E NOTIFICO IL CAMBIAMENTO NELLO SLOT
            if (canInsert)
            {
                items.Push(item);
                OnInventorySlotChanged?.Invoke(this, Item);
            }
            //RITORNO SE SON RIUSCITO AD INSERIRE
            return canInsert;
        }

        public virtual Item RetrieveItem()
        {
            Item retrievedItem = null;
            //SE HO DEGLI OGGETTI
            if (Item != null)
            {
                //MI PRENDO L'OGGETTO CHE STA AL TOP DELLO STACK
                retrievedItem = items.Pop();
                //NOTIFICO IL CAMBIAMENTO
                OnInventorySlotChanged?.Invoke(this, retrievedItem);
            }
            //RITORNO L'OGGETTO SE PRESENTE, ALTRIMENTI NULL
            return retrievedItem;
        }
    }
}