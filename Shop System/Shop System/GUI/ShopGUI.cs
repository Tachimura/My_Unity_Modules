using InventorySystem.GUI;
using InventorySystem.Items;
using UnityEngine;

namespace ShopSystem.ShopSystem.GUI
{
    public class ShopGUI : AShopGUI
    {
        private ShopSlotGUI[] inventorySlots = null;

        [SerializeField]
        protected DescribablePanelController describablePanel = null;

        public override void LoadShop(IShop shop)
        {
            base.LoadShop(shop);
            //PREPARO L'INVENTARIO NUOVO
            ShopManager manager = ShopManager as ShopManager;
            inventorySlots = new ShopSlotGUI[ShopManager.ShopSize];
            for (int cont = 0; cont < ShopManager.ShopSize; cont++)
            {
                GameObject slot = Instantiate(slotPrefab, scrollRect.content.transform);
                slot.transform.SetParent(scrollRect.content.transform, false);
                inventorySlots[cont] = slot.GetComponent<ShopSlotGUI>();
                inventorySlots[cont].SetInventorySlot(manager.InventorySlotAtPosition(cont) as ShopSlot);
                PlayerOwnedCount[cont] = playerDataWrapper.Inventory.RetrieveItemCount(inventorySlots[cont].PeekItem());
            }
            //IMPOSTO IL PRIMO ELEMENTO COME SELEZIONATO E NE MOSTRO LE INFORMAZIONI
            inventorySlots[0].Select();
            ShowItemInfo(inventorySlots[0].PeekItem());
        }

        protected override void ResetShop()
        {
            //PULIZIA INFORMAZIONI DELL'INVENTARIO VECCHIE SE SONO PRESENTI
            if (inventorySlots != null)
                foreach (IInventoryGUISlot slot in inventorySlots)
                    slot.Reset();
        }

        public override Item AcquireSelectedItem()
        {
            Item item = inventorySlots[CurrentSlotPosition].PeekItem();
            //SE C'è UN OGGETTO
            if (item != null && inventorySlots[CurrentSlotPosition].CanRetrieveItem())
            {
                //TODO POSSO METTERE GLI SCONTI SE CI SONO, MOLTIPLICANDO item.ItemValue PER UN VALORE
                int itemPrice = ItemShopCost(item);
                //SE IL PLAYER PUò ACQUISTARLO
                if (playerDataWrapper.Money >= itemPrice)
                {
                    //CREO L'ISTANZA E LA ACQUISTO
                    playerDataWrapper.BuyItem(Instantiate(inventorySlots[CurrentSlotPosition].RetrieveItem()), itemPrice);
                    //RITORNO LA DESCRIZIONE DELL'OGGETTO ACQUISTATO DAL PLAYER
                    return item;
                }
            }
            return null;
        }
        public override void Move(Vector2 direction)
        {
            if (direction.magnitude > 0.1)
            {
                //NORMALIZZO L'INPUT
                NormalizeMoveInput(ref direction);
                //AGGIORNO IL CURRENT SELECTED SLOT
                int newPosition = CurrentSlotPosition - (int)direction.y;
                //CONTROLLI PER LA NUOVA POSIZIONE
                if (newPosition < 0)
                    newPosition = 0;
                else if (newPosition >= inventorySlots.Length)
                    newPosition = inventorySlots.Length - 1;
                //SE LA NUOVA POSIZIONE è DIFFERENTE DA QUELLA ATTUALE
                if (newPosition != CurrentSlotPosition)
                {
                    //DESELEZIONO L'ATTUALE INVENTORY SLOT
                    inventorySlots[CurrentSlotPosition].Deselect();
                    //MUOVO IL RECT TRANSFORM DELLO SCROLL LAYOUT
                    //AGGIORNO CON LA NUOVA POSIZIONE
                    PreviousSlotPosition = CurrentSlotPosition;
                    CurrentSlotPosition = newPosition;
                    CenterOnItem(inventorySlots[CurrentSlotPosition].GetComponent<RectTransform>());
                    //SELEZIONO IL NUOVO ATTUALE INVENTORY SLOT
                    inventorySlots[CurrentSlotPosition].Select();
                    ShowItemInfo(inventorySlots[CurrentSlotPosition].PeekItem());
                }
            }
        }

        private int ItemShopCost(Item item)
        {
            ShopManager manager = ShopManager as ShopManager;
            return (int)(item.ItemValue * ((float)manager.ShopPrice / 100));
        }

        protected override void ShowItemInfo(Item item)
        {
            if (item == null)
            {
                describablePanel.enabled = false;
                describablePanel.SetDescription(null);
                playerOwnedText.text = "0";
                itemCostText.text = "0";
            }
            else
            {
                describablePanel.enabled = true;
                describablePanel.SetDescription(item);
                playerOwnedText.text = PlayerOwnedCount[CurrentSlotPosition].ToString();
                itemCostText.text = ItemShopCost(item).ToString();
            }
        }

        protected override void PlayerInventoryItemChanged(Item item)
        {
            int position = -1;
            foreach (IInventoryGUISlot iSlot in inventorySlots)
            {
                if (iSlot.PeekItem().EqualsTo(item))
                {
                    position++;
                    break;
                }
                position++;
            }
            if (position != -1)
            {
                PlayerOwnedCount[position] = playerDataWrapper.Inventory.RetrieveItemCount(item);
                ShowItemInfo(inventorySlots[CurrentSlotPosition].PeekItem());
            }
        }
    }
}