using InventorySystem.Items;
using System;

namespace InventorySystem
{
    public interface IInventorySlot
    {
        //EVENTO CHE NOTIFICA CHE C'è STATO UN CAMBIAMENTO DENTRO DI ME
        event Action<IInventorySlot, Item> OnInventorySlotChanged;
        //L'OGGETTO CONTENUTO NELLO SLOT
        Item Item { get; }
        //NUMERO DI OGGETTI NELLO STACK PRESENTI NELLO SLOT
        int ItemCount { get; }
        //INSERIRE UN OGGETTO NELLO SLOT CON LA DESCRIZIONE PASSATA IN INGRESSO
        bool InsertItem(Item item);
        //PRENDE UNA UNITà DELL'OGGETTO NELLO SLOT
        Item RetrieveItem();
    }
}