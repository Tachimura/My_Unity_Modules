using InventorySystem.Items;
using System;
using System.Collections.Generic;

namespace InventorySystem
{
    [Serializable]
    public class Inventory : IInventory
    {
        public event Action<Item> OnInventoryItemChanged;
        public event Action<IInventorySlot, bool> OnInventorySlotStatusChanged;

        public int InventorySize => slots.Count;

        public int InventorySlotMaxStackSize => 99;

        private readonly List<IInventorySlot> slots = null;
        public Inventory() => slots = new List<IInventorySlot>();

        public Inventory(int nSlots) : this()
        {
            for (int cont = 0; cont < nSlots; cont++)
                CreateNewSlot();
        }

        private IInventorySlot CreateNewSlot()
        {
            IInventorySlot slot = new InventorySlot();
            slot.OnInventorySlotChanged += InventorySlotChanged;
            slots.Add(slot);
            return slot;
        }

        private void InventorySlotChanged(IInventorySlot slot, Item item)
        {
            if (slot.Item == null)
            {
                InventorySlotStatusChanged(slot, false);
                slot.OnInventorySlotChanged -= InventorySlotChanged;
                slots.Remove(slot);
            }
            OnInventoryItemChanged?.Invoke(item);
        }
        private void InventorySlotStatusChanged(IInventorySlot slot, bool status)
        {
            OnInventorySlotStatusChanged?.Invoke(slot, status);
        }

        public virtual bool ContainItem(Item item)
        {
            bool itemFound = false;
            //CERCO IN OGNI SLOT
            foreach (IInventorySlot slot in slots)
                //SE LO SLOT HA UN ITEM, E L'ITEM è QUELLO CHE CERCO
                if (slot.Item != null && slot.Item.EqualsTo(item))
                {
                    //DICO CHE HO TROVATO L'ITEM E FERMO LA RICERCA
                    itemFound = true;
                    break;
                }
            return itemFound;
        }

        public virtual Item RetrieveItem(Item item)
        {
            Item retrievedItem = null;
            //CERCO IN OGNI SLOT
            foreach (IInventorySlot slot in slots)
                //SE LO SLOT HA UN ITEM, E L'ITEM è QUELLO CHE VOGLIO
                if (slot.Item != null && slot.Item.EqualsTo(item))
                {
                    //MI PRENDO L'ITEM E FERMO LA RICERCA
                    retrievedItem = slot.RetrieveItem();
                    break;
                }
            return retrievedItem;
        }

        public virtual Item RetrieveItem(int slotPosition)
        {
            Item retrievedItem = null;
            //SE LA POSIZIONE VOLUTA è VALIDA
            if (slotPosition >= 0 && slotPosition < InventorySize)
                //RITORNO L'OGGETTO IN QUELLA POSIZIONE
                retrievedItem = slots[slotPosition].RetrieveItem();
            return retrievedItem;
        }


        public virtual bool InsertItem(Item item)
        {
            //CERCO UNO SLOT DISPONIBILE
            foreach (IInventorySlot slot in slots)
                //SE UNO SLOT MI PERMETTE L'INSERIMENTO ALLORA TERMINO LA RICERCA
                if (slot.InsertItem(item))
                    return true;
            //ALTRIMENTI MI CREO UN NUOVO SLOT
            IInventorySlot newSlot = CreateNewSlot();
            //E CI INSERICO L'OGGETTO
            newSlot.InsertItem(item);
            InventorySlotStatusChanged(newSlot, true);
            return true;
        }

        public virtual int RetrieveItemCount(Item item)
        {
            int itemCount = 0;
            //CERCO IN TUTTI GLI SLOT GLI ELEMENTI item CHE CI SONO
            foreach (IInventorySlot slot in slots)
                //SE LO SLOT HA UN ITEM, E L'ITEM è QUELLO CHE VOGLIO
                if (slot.Item != null && slot.Item.EqualsTo(item))
                    //INCREMENTO IL NUMERO DI ITEMS TROVATI DEL NUMERO DI ITEMS PRESENTI NELLO SLOT
                    itemCount += slot.ItemCount;
            return itemCount;
        }

        public virtual IInventorySlot InventorySlotAtPosition(int position)
        {
            if (position >= 0 && position < InventorySize)
                return slots[position];
            return null;
        }
    }
}