using UnityEngine;
using UtilityInterface;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/Consumables/Potions/HealthPotion")]
    public class HealthPotion : Item, IUseable
    {
        [SerializeField]
        private int health = 0;
        private PlayerDataManager playerData;

        public override string DescribableName { get; protected set; }
        public override string DescribableDescription { get; protected set; }

        private void Awake()
        {
            //TODO: DA MODIFICARE NON MI PIACE MOLTO
            //TODO:! USARE RISORSE
            playerData = PlayerDataManager.Instance;
            if (playerData == null)
                playerData = FindObjectOfType<PlayerDataManager>();
            DescribableName = "<color=red>" + ItemName + "</color>";
            DescribableDescription = "Rigenera " + health + " HP";
        }

        public bool CanUse()
        {
            if (playerData.PlayerDataWrapper.PlayerHP < playerData.PlayerDataWrapper.PlayerMaxHP)
                return true;
            else
                return false;
        }

        public void Use()
        {
            playerData.PlayerDataWrapper.PlayerHP += health;
            Destroy(this);
        }
    }
}