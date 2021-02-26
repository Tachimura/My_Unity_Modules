using InventorySystem.Items;
using System;

namespace InventorySystem
{
    public interface IInventory
    {
        //EVENTO CHE NOTIFICA CHE UNO DEI MIEI SLOT è CAMBIATO
        event Action<Item> OnInventoryItemChanged;
        //EVENTO CHE NOTIFICA CHE UNO DEI MIEI SLOT SI ATTIVATO O DISATTIVATO
        event Action<IInventorySlot, bool> OnInventorySlotStatusChanged;
        int InventorySize { get; }
        //INDICA LA DIMENSIONE MASSIMA CHE LO STACK DI UNO SLOT PUò ACQUISIRE
        int InventorySlotMaxStackSize { get; }
        //INSERIRE UN ITEM CON DESCRIZIONE IN INGRESSO
        bool InsertItem(Item item);
        //PRENDERE ITEM ALLA POSIZIONE slotPosition
        Item RetrieveItem(int slotPosition);
        //PRENDERE UN CERTO ITEM CON DESCRIZIONE IN INGRESSO
        Item RetrieveItem(Item item);
        //RITORNA IL NUMERO DI OGGETTI UGUALI A QUELLO INDICATO
        int RetrieveItemCount(Item item);
        //SAPERE SE è PRESENTE UN CERTO ITEM CON DESCRIZIONE IN INGRESSO
        bool ContainItem(Item item);
        //MI DA L'INVENTORY SLOT NELLA POSIZIONE PASSATA IN INGRESSO
        IInventorySlot InventorySlotAtPosition(int position);
    }
}