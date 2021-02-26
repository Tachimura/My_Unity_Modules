using UnityEngine;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Equipments/Armor")]
    public class Armor : Equipment
    {
        //VALORE DI ATTACCO DELL'ARMA
        [SerializeField]
        [Min(1)]
        protected int defenseValue = 1;
        public int DefenseValue => defenseValue;

        //
        public Armor() : base() => EquipmentSlot = EquipmentSlot.Armor;

        protected override void CreateEquipmentDescription()
        {
            //TODO:! USARE RISORSE
            DescribableDescription =
                CreateHeaderText() +
                "Defense Value: " + DefenseValue.ToString() + " \n" +
                CreateModifiersText();
        }
    }
}