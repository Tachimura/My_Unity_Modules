using InventorySystem;
using InventorySystem.GUI;
using InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.ShopSystem.GUI
{
    public class ShopSlotGUI : MonoBehaviour, IInventoryGUISlot
    {
        //SLOT DELL'INVENTARIO A CUI FACCIO RIFERIMENTO
        private ShopSlot slot = null;

        [SerializeField]
        private Image itemImage = null;
        [SerializeField]
        private Image selectedBackground = null;
        [SerializeField]
        private TextMeshProUGUI itemName = null;
        [SerializeField]
        private TextMeshProUGUI itemStackCount = null;

        private void OnDestroy() => Reset();

        public void Reset()
        {
            //RIMUOVO LA SELEZIONE SE ERA PRESENTE
            Deselect();
            //SE LO SLOT è IMPOSTATO DEVO DEIMPOSTARLO
            if (slot != null)
            {
                //RIMUOVO IL LISTENER ALL'EVENTO
                slot.OnInventorySlotChanged -= UpdateSlotUI;
                //RIMUOVO IL COLLEGAMENTO
                slot = null;
            }
        }
        public void Select() => selectedBackground.enabled = true;
        public void Deselect() => selectedBackground.enabled = false;

        public bool SetInventorySlot(ShopSlot slot)
        {
            //SE QUESTO SLOT è UTILIZZABILE
            if (this.slot == null)
            {
                this.slot = slot;
                this.slot.OnInventorySlotChanged += UpdateSlotUI;
                UpdateSlotUI(slot, slot.Item);
            }
            //QUESTO SLOT è GIà STATO ALLOCATO
            return false;
        }
        public Item PeekItem() => slot?.Item;
        public bool CanRetrieveItem() => slot != null && (slot.ItemUnlimited || slot.ItemCount > 0);
        public Item RetrieveItem() => slot?.RetrieveItem();

        private void UpdateSlotUI(IInventorySlot slot, Item item)
        {
            //SE C'è UN OGGETTO
            if (slot.Item != null)
            {
                itemImage.sprite = slot.Item.DescribableIcon;
                itemImage.enabled = true;
                //ATTIVO IL TESTO
                itemStackCount.enabled = true;
                //SE è ILLIMITATO TOLGO IL TESTO
                if ((slot as ShopSlot).ItemUnlimited)
                    //TODO: USARE RISORSE
                    itemStackCount.text = "Illimitati";
                else
                {
                    //TODO: USARE RISORSE
                    //METTO IL TESTO UGUALE AL NUMERO DI ITEMS
                    itemStackCount.text = "Rimanenti: " + slot.ItemCount.ToString();
                }
                itemName.enabled = true;
                itemName.text = slot.Item.name;
            }
            //SE NON CI SONO OGGETTI NELLO SLOT
            else
            {
                //DISATTIVO L'IMMAGINE
                itemImage.sprite = null;
                itemImage.enabled = false;
                //DISATTIVO IL TESTO
                itemStackCount.enabled = false;
                itemName.enabled = false;
            }
        }
    }
}