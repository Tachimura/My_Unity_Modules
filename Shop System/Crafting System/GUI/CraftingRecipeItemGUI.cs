using InventorySystem.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem.CraftingSystem.GUI
{
    public class CraftingRecipeItemGUI : MonoBehaviour
    {
        [SerializeField]
        private Image itemIcon = null;
        [SerializeField]
        private TextMeshProUGUI itemName = null;
        [SerializeField]
        private TextMeshProUGUI itemRequired = null;
        public Item CraftingItem => craftingItem.Item != null ? craftingItem.Item : null;
        public bool CanCraft => playerItemAmount >= craftingItem.Amount;

        private CraftingItem craftingItem;
        private int playerItemAmount = 0;
        public void SetCraftingItem(CraftingItem craftingItem)
        {
            this.craftingItem = craftingItem;
            itemIcon.sprite = craftingItem.Item.DescribableIcon;
            itemName.text = craftingItem.Item.DescribableName;
        }

        public void UpdatePlayerItemAmount(int playerItemAmount)
        {
            //MODIFICO LA QUANTITà DI RISORSE POSSEDUTE
            this.playerItemAmount = playerItemAmount;
            //TODO USARE RISORSE
            itemRequired.text = "Required: " + playerItemAmount + "/" + craftingItem.Amount;
        }
    }
}