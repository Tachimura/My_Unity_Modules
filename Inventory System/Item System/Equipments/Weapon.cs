using UnityEngine;

namespace InventorySystem.Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Equipments/Weapon")]
    public class Weapon : Equipment
    {
        //VALORE DI ATTACCO DELL'ARMA
        [SerializeField]
        [Min(1)]
        protected int attackValue = 1;
        public int AttackValue => attackValue;

        [SerializeField]
        private string oh_light_attack_01;
        public string OH_Light_Attack_01 => oh_light_attack_01;
        [SerializeField]
        private string oh_heavy_attack_01;
        public string OH_Heavy_Attack_01 => oh_heavy_attack_01;

        //
        public Weapon() : base() => EquipmentSlot = EquipmentSlot.Weapon;

        protected override void CreateEquipmentDescription()
        {
            //TODO:! USARE RISORSE
            DescribableDescription =
                CreateHeaderText() +
                "Attack Value: " + AttackValue.ToString() + " \n" +
                CreateModifiersText();
        }
    }
}