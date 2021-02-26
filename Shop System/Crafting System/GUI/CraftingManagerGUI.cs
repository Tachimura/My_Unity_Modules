using InventorySystem.GUI;
using InventorySystem.Items;
using UnityEngine;

namespace ShopSystem.CraftingSystem.GUI
{
    public class CraftingManagerGUI : AShopGUI
    {
        [SerializeField]
        private CraftingRecipeGUI recipeGUI = null;

        protected override void ResetShop()
        {
            //PULIZIA INFORMAZIONI DELL'INVENTARIO VECCHIE SE SONO PRESENTI
            if (shopSlots != null)
                foreach (IInventoryGUISlot slot in shopSlots)
                    slot.Reset();
        }
        public override void LoadShop(IShop shop)
        {
            base.LoadShop(shop);
            //PREPARO L'INVENTARIO NUOVO
            CraftingManager manager = ShopManager as CraftingManager;
            shopSlots = new CraftingRecipeSlotGUI[manager.Recipes.Length];
            for (int cont = 0; cont < manager.Recipes.Length; cont++)
            {
                GameObject slot = Instantiate(slotPrefab, scrollRect.content.transform);
                slot.transform.SetParent(scrollRect.content.transform, false);
                shopSlots[cont] = slot.GetComponent<CraftingRecipeSlotGUI>();
                (shopSlots[cont] as CraftingRecipeSlotGUI).SetRecipe(manager.Recipes[cont]);
                PlayerOwnedCount[cont] = playerDataWrapper.Inventory.RetrieveItemCount(manager.Recipes[cont].Result);
            }
            shopSlots[CurrentSlotPosition].Select();
            ShowRecipeInfo((shopSlots[CurrentSlotPosition] as CraftingRecipeSlotGUI).Recipe);
            ShowItemInfo(shopSlots[CurrentSlotPosition].PeekItem());
        }

        protected override void PlayerInventoryItemChanged(Item item)
        {
            base.PlayerInventoryItemChanged(item);
            int itemCount = playerDataWrapper.Inventory.RetrieveItemCount(item);
            ShowItemInfo(shopSlots[CurrentSlotPosition].PeekItem());
            recipeGUI.UpdateItemCount(item, itemCount);
        }

        public override Item AcquireSelectedItem()
            => (ShopManager as CraftingManager).AcquireItem(playerDataWrapper.Inventory, CurrentSlotPosition);

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
                else if (newPosition >= shopSlots.Length)
                    newPosition = shopSlots.Length - 1;
                //SE LA NUOVA POSIZIONE è DIFFERENTE DA QUELLA ATTUALE
                if (newPosition != CurrentSlotPosition)
                {
                    //DESELEZIONO L'ATTUALE INVENTORY SLOT
                    shopSlots[CurrentSlotPosition].Deselect();
                    //MUOVO IL RECT TRANSFORM DELLO SCROLL LAYOUT
                    //AGGIORNO CON LA NUOVA POSIZIONE
                    PreviousSlotPosition = CurrentSlotPosition;
                    CurrentSlotPosition = newPosition;
                    CenterOnItem((shopSlots[CurrentSlotPosition] as CraftingRecipeSlotGUI).GetComponent<RectTransform>());
                    //SELEZIONO IL NUOVO ATTUALE INVENTORY SLOT
                    shopSlots[CurrentSlotPosition].Select();
                    ShowRecipeInfo((shopSlots[CurrentSlotPosition] as CraftingRecipeSlotGUI).Recipe);
                }
            }
        }

        protected override void ShowItemInfo(Item item)
        {
            if (item == null)
            {
                playerOwnedText.text = "0";
            }
            else
            {
                playerOwnedText.text = PlayerOwnedCount[CurrentSlotPosition].ToString();
            }
        }

        private void ShowRecipeInfo(CraftingRecipe recipe)
        {
            if (recipe == null)
            {
                playerOwnedText.text = "0";
                recipeGUI.SetRecipe(null, null);
            }
            else
            {
                playerOwnedText.text = PlayerOwnedCount[CurrentSlotPosition].ToString();
                recipeGUI.SetRecipe(recipe, playerDataWrapper.Inventory);
            }
        }
    }
}