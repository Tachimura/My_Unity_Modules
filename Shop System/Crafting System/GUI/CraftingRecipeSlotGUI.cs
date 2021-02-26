using InventorySystem.GUI;
using InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.CraftingSystem.GUI
{
    public class CraftingRecipeSlotGUI : MonoBehaviour, IInventoryGUISlot
    {
        //RECIPE A CUI FACCIO RIFERIMENTO
        public CraftingRecipe Recipe { get; private set; } = null;

        [SerializeField]
        private Image itemImage = null;
        [SerializeField]
        private Image selectedBackground = null;
        [SerializeField]
        private TextMeshProUGUI itemName = null;

        private void OnDestroy() => Reset();

        public void Reset()
        {
            //RIMUOVO LA SELEZIONE SE ERA PRESENTE
            Deselect();
            //SE LO SLOT è IMPOSTATO DEVO DEIMPOSTARLO
            if (Recipe != null)
            {
                //RIMUOVO IL LISTENER ALL'EVENTO
                //slot.OnInventorySlotChanged -= UpdateSlotUI;
                //RIMUOVO IL COLLEGAMENTO
                Recipe = null;
            }
        }

        public void Select() => selectedBackground.enabled = true;
        public void Deselect() => selectedBackground.enabled = false;

        public Item PeekItem() => Recipe != null && Recipe.Result != null ? Recipe.Result : null;
        public Item RetrieveItem() => null;

        public bool SetRecipe(CraftingRecipe recipe)
        {
            //SE QUESTO SLOT è UTILIZZABILE
            if (Recipe == null && recipe != null)
            {
                Recipe = recipe;
                UpdateSlotUI();
                return true;
            }
            //QUESTO SLOT è GIà STATO ALLOCATO
            return false;
        }

        private void UpdateSlotUI()
        {
            //SE C'è UNA RECIPE
            if (Recipe != null)
            {
                itemImage.enabled = true;
                itemImage.sprite = Recipe.Result.DescribableIcon;

                itemName.enabled = true;
                itemName.text = Recipe.Result.name;
            }
            //SE NON CI SONO OGGETTI NELLO SLOT
            else
            {
                //DISATTIVO L'IMMAGINE
                itemImage.sprite = null;
                itemImage.enabled = false;
                //DISATTIVO IL TESTO
                itemName.enabled = false;
            }
        }
    }
}