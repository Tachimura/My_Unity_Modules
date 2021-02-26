using InventorySystem.GUI;
using TMPro;
using UnityEngine;

namespace InventorySystem.Player
{
    public class PlayerInventoryGUI : InventoryGUI
    {
        [SerializeField]
        private TextMeshProUGUI moneyText = null;
        private PlayerDataWrapper playerDataWrapper;

        public void LoadInventory(PlayerDataWrapper playerDataWrapper)
        {
            //PULISCO LE VECCHIE COSE
            ResetInventory();
            //COLLEGAMENTO AI DATI NUOVI
            this.playerDataWrapper = playerDataWrapper;
            this.playerDataWrapper.OnPlayerMoneyChanged += InventoryMoneyChanged;
            //CARICAMENTO DI INVENTARIO E SOLDI BASE
            LoadInventory(playerDataWrapper.Inventory);
            InventoryMoneyChanged(playerDataWrapper.Money);
        }

        private void OnDestroy() => ResetInventory();

        private void ResetInventory()
        {
            if (playerDataWrapper != null)
            {
                playerDataWrapper.OnPlayerMoneyChanged -= InventoryMoneyChanged;
                playerDataWrapper = null;
            }
        }

        private void InventoryMoneyChanged(int newAmount)
        {
            //IMPOSTO I SOLDI NEL TESTO, SE è DIVERSO DA NULL
            if (moneyText != null)
                //TODO USARE RISORSE
                moneyText.text = "Golds: " + newAmount.ToString();
        }
    }
}