using InventorySystem;
using InventorySystem.Items;
using TMPro;
using UnityEngine;

namespace ShopSystem.CraftingSystem.GUI
{
    public class CraftingRecipeGUI : MonoBehaviour
    {
        [SerializeField]
        private CraftingRecipeItemGUI[] materialsGUI = null;
        [SerializeField]
        private DescribablePanelController describablePanel = null;
        [SerializeField]
        private TextMeshProUGUI craftButtonLabel = null;

        private CraftingRecipe recipe = null;

        public void SetRecipe(CraftingRecipe recipe, IInventory playerInventory)
        {
            if (recipe != null && playerInventory != null)
            {
                //IMPOSTO LA RECIPE IN USO
                this.recipe = recipe;
                //IMPOSTO I VARI MATERIALI RICHIESTI
                int materialsCount = recipe.Materials.Length;
                for (int cont = 0; cont < materialsGUI.Length; cont++)
                {
                    if (cont < materialsCount)
                    {
                        materialsGUI[cont].gameObject.SetActive(true);
                        materialsGUI[cont].SetCraftingItem(recipe.Materials[cont]);
                        materialsGUI[cont].UpdatePlayerItemAmount(playerInventory.RetrieveItemCount(recipe.Materials[cont].Item));
                    }
                    else
                        materialsGUI[cont].gameObject.SetActive(false);
                }
                //IMPOSTO LA DESCRIZIONE DEL RISULTATO
                describablePanel.SetDescription(recipe.Result);
            }
            else
            {
                this.recipe = null;
                foreach (CraftingRecipeItemGUI materialGUI in materialsGUI)
                    materialGUI.gameObject.SetActive(false);
                describablePanel.SetDescription(null);
            }
            UpdateCraftButtonText();
        }
        public void UpdateItemCount(Item item, int itemCount)
        {
            //SE LA RECIPE è IMPOSTATA
            if (recipe != null)
            {
                if (item != null)
                {
                    //PER OGNI MATERIALE RICHIESTO DALLA RECIPE
                    foreach (CraftingRecipeItemGUI materialGUI in materialsGUI)
                    {
                        //SE è ABILITATO E SE HA LO STESSO OGGETTO CHE IL PLAYER HA APPENA GUADAGNATO
                        if (materialGUI.gameObject.activeSelf && materialGUI.CraftingItem.EqualsTo(item))
                            //AGGIORNO IL VALORE
                            materialGUI.UpdatePlayerItemAmount(itemCount);
                    }
                }
                UpdateCraftButtonText();
            }
        }

        private void UpdateCraftButtonText()
        {
            //TODO: USARE RISORSE
            if (craftButtonLabel != null)
            {
                //SE LA RECIPE è IMPOSTATA
                if (recipe != null)
                {
                    bool canCraft = true;
                    //PER OGNI MATERIALGUI
                    foreach (CraftingRecipeItemGUI materialGUI in materialsGUI)
                        //SE è USATO NELLA RECIPE MA NON HO ABBASTANZA MATERIALI
                        if (materialGUI.gameObject.activeSelf && !materialGUI.CanCraft)
                        {
                            //NON POSSO CRAFTARE L'OGGETTO
                            canCraft = false;
                            break;
                        }
                    //INDICO SE POSSO CRAFTARE O MENO L'OGGETTO
                    if (canCraft)
                        craftButtonLabel.text = "B to Craft";
                    else
                        craftButtonLabel.text = "Cannot Craft";
                }
                //SE LA RECIPE NON è IMPOSTATA
                else
                {
                    craftButtonLabel.text = "Recipe Not Defined";
                }
            }
        }
    }
}