using InventorySystem.Items;
using UnityEngine;

namespace InventorySystem.GUI
{
    public interface IInventoryGUI
    {
        //L'INVENTARIO DI CUI GESTISCO LA GUI
        IInventory Inventory { get; }
        //LO SLOT ATTUALMENTE SELEZIONATO
        int CurrentSelectedSlotPosition { get; }
        //CARICARE L'INVENTARIO
        void LoadInventory(IInventory inventory);
        //SPOSTARSI NELL'INVENTARIO
        void Move(Vector2 direction);
        //VEDERE IL SELECTED ITEM
        Item PeekSelected();
        //PRENDERE IL SELECTED ITEM
        Item RetrieveSelected();
    }
}