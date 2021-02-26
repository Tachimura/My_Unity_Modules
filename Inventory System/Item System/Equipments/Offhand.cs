using UnityEngine;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Offhand", menuName = "Items/Equipments/Offhand")]
    public class Offhand : Equipment
    {
        //VALORE DI ATTACCO DELLA OFFHAND
        [SerializeField]
        [Min(1)]
        protected int attackValue = 1;
        public int AttackValue => attackValue;

        //VALORE DI DIFESA DELLA OFFHAND
        [SerializeField]
        [Min(1)]
        protected int defenseValue = 1;
        public int DefenseValue => defenseValue;

        //
        public Offhand() : base() => EquipmentSlot = EquipmentSlot.Offhand;

        protected override void CreateEquipmentDescription()
        {
            //TODO:! USARE RISORSE
            DescribableDescription =
                CreateHeaderText() +
                "Attack Value: " + AttackValue.ToString() + " \n" +
                "Defense Value: " + DefenseValue.ToString() + " \n" +
                CreateModifiersText();
        }
    }
}