using InventorySystem.Items;
using System;

namespace InventorySystem.Player
{
    [Serializable]
    public class PlayerInventory
    {
        public IInventory Inventory { get; private set; } = null;
        public int Money { get; private set; } = 0;

        public event Action<int> OnMoneyChanged;
        //OLD EQUIP, NEW EQUIP
        public event Action<Equipment, Equipment> OnEquipmentChanged;

        public InventorySlot EquippedWeaponSlot { get; private set; } = null;
        public InventorySlot EquippedOffhandSlot { get; private set; } = null;
        public InventorySlot EquippedArmorSlot { get; private set; } = null;
        public PlayerInventory(IInventory inventory, int money)
        {
            Inventory = inventory;
            Money = money;

            EquippedWeaponSlot = new InventorySlot();
            EquippedOffhandSlot = new InventorySlot();
            EquippedArmorSlot = new InventorySlot();
        }

        private void MoneyChanged() => OnMoneyChanged?.Invoke(Money);

        public bool BuyItem(Item item, int price)
        {
            if (Money >= price && Inventory.InsertItem(item))
            {
                Money -= price;
                MoneyChanged();
                return true;
            }
            return false;
        }

        public void Equip(Equipment equip)
        {
            Equipment oldEquipment = null;
            switch (equip.EquipmentSlot)
            {
                case EquipmentSlot.Weapon:
                    {
                        if (EquippedWeaponSlot != null)
                        {
                            oldEquipment = EquippedWeaponSlot.RetrieveItem() as Equipment;
                            Inventory.InsertItem(oldEquipment);
                        }
                        EquippedWeaponSlot.InsertItem(equip);
                        break;
                    }
                case EquipmentSlot.Offhand:
                    {
                        if (EquippedOffhandSlot != null)
                        {
                            oldEquipment = EquippedOffhandSlot.RetrieveItem() as Equipment;
                            Inventory.InsertItem(oldEquipment);
                        }
                        EquippedOffhandSlot.InsertItem(equip);
                        break;
                    }
                case EquipmentSlot.Armor:
                    {
                        if (EquippedArmorSlot != null)
                        {
                            oldEquipment = EquippedArmorSlot.RetrieveItem() as Equipment;
                            Inventory.InsertItem(oldEquipment);
                        }
                        EquippedArmorSlot.InsertItem(equip);
                        break;
                    }
            }
            OnEquipmentChanged?.Invoke(oldEquipment, equip);
        }
    }
}