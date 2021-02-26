using InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.GUI
{
    public class InventoryGUISlot : MonoBehaviour, IInventoryGUISlot
    {
        //SLOT DELL'INVENTARIO A CUI FACCIO RIFERIMENTO
        public IInventorySlot InventorySlot { get; private set; }

        [SerializeField]
        private Image itemImage = null;
        [SerializeField]
        private Image selectedBackground = null;
        [SerializeField]
        private TextMeshProUGUI itemStackCount = null;

        public bool SetInventorySlot(IInventorySlot slot)
        {
            //SE QUESTO SLOT è UTILIZZABILE
            if (InventorySlot == null)
            {
                InventorySlot = slot;
                InventorySlot.OnInventorySlotChanged += UpdateSlotUI;
                UpdateSlotUI(InventorySlot, slot.Item);
            }
            //QUESTO SLOT è GIà STATO ALLOCATO
            return false;
        }

        public void Reset()
        {
            //RIMUOVO LA SELEZIONE SE ERA PRESENTE
            Deselect();
            //SE LO SLOT è IMPOSTATO DEVO DEIMPOSTARLO
            if (InventorySlot != null)
            {
                //RIMUOVO IL LISTENER ALL'EVENTO
                InventorySlot.OnInventorySlotChanged -= UpdateSlotUI;
                //RIMUOVO IL COLLEGAMENTO
                InventorySlot = null;
            }
        }

        public void Select()
        {
            selectedBackground.enabled = true;
        }
        public void Deselect()
        {
            selectedBackground.enabled = false;
        }

        public Item PeekItem()
        {
            if (InventorySlot != null && InventorySlot.Item != null)
                return InventorySlot.Item;
            return null;
        }

        public Item RetrieveItem()
        {
            if (InventorySlot != null && InventorySlot.Item != null)
                return InventorySlot.RetrieveItem();
            return null;
        }

        private void OnDestroy() => Reset();

        private void UpdateSlotUI(IInventorySlot slot, Item item)
        {
            //SE C'è UN OGGETTO
            if (slot.Item != null)
            {
                itemImage.sprite = slot.Item.DescribableIcon;
                itemImage.enabled = true;
                //SE L'OGGETTO è STACKABILE ED HO PIù DI 1 ITEM
                if (item.IsStackable && slot.ItemCount > 1)
                {
                    //METTO IL TESTO UGUALE AL NUMERO DI ITEMS
                    itemStackCount.text = slot.ItemCount.ToString();
                    //ATTIVO IL TESTO
                    itemStackCount.enabled = true;
                }
                //ALTRIMENTI DISATTIVO IL TESTO
                else
                    itemStackCount.enabled = false;
            }
            //SE NON CI SONO OGGETTI NELLO SLOT
            else
            {
                //DISATTIVO L'IMMAGINE
                itemImage.sprite = null;
                itemImage.enabled = false;
                //DISATTIVO IL TESTO
                itemStackCount.enabled = false;
            }
        }
    }
}