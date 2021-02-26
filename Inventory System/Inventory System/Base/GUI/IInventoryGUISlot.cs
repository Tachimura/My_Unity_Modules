using InventorySystem.Items;

namespace InventorySystem.GUI
{
    public interface IInventoryGUISlot
    {
        //PER RIMUOVERE LA REFERENZA ALL'ITEM SLOT DELL'INVENTARIO
        void Reset();
        //PER INDICARE CHE QUESTO è LO SLOT ATTUALMENTE SELEZIONATO
        void Select();
        //PER INDICARE CHE QUESTO NON è PIù LO SLOT ATTUALMENTE SELEZIONATO
        void Deselect();
        //PER VEDERE L'OGGETTO IN QUESTO SLOT
        Item PeekItem();
        //PER PRENDERE L'OGGETTO IN QUESTO SLOT
        Item RetrieveItem();
    }
}