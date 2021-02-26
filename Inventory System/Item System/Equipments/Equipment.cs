using CharacterDataSystem;
using UnityEngine;

namespace InventorySystem.Items
{
    public abstract class Equipment : Item
    {
        //LIVELLO MINIMO PER POTER USARE L'EQUIPAGGIAMENTO
        [SerializeField]
        [Min(1)]
        protected int minLevel = 1;
        public int MinLevel => minLevel;

        //TIPO DI SLOT SU CUI ANDRO AD USARE QUESTO EQUIPAGGIAMENTO
        public EquipmentSlot EquipmentSlot { get; protected set; } = EquipmentSlot.Weapon;

        public override string DescribableName { get; protected set; }
        public override string DescribableDescription { get; protected set; }

        [SerializeField]
        private StatModifier[] modifiers = null;
        public StatModifier[] StatsModifiers => modifiers ?? null;
        //PREFAB DELL'ARMA
        [SerializeField]
        private GameObject equipPrefab = null;
        public GameObject EquipPrefab => equipPrefab;

        protected virtual void Awake()
        {
            //TODO NON MI PIACE MOLTO
            if (modifiers != null)
                foreach (StatModifier modifier in modifiers)
                    modifier.Source = this;
            //CREO LA DESCRIZIONE DELL'EQUIPAGGIAMENTO
            CreateEquipmentDescription();
        }

        protected abstract void CreateEquipmentDescription();

        protected string CreateHeaderText()
        {
            DescribableName = ItemName;
            ItemType = ItemType.Equipment;
            //TODO: USARE RISORSE
            //IMPOSTO LA DESCRIZIONE DELL'OGGETTO
            string strLevel = "Livello Richiesto: " + minLevel.ToString();
            string strSlot = "Slot Equipaggiabile: " + ((EquipmentSlot == EquipmentSlot.Weapon) ? "Weapon" : ((EquipmentSlot == EquipmentSlot.Offhand) ? "Weapon 2" : "Armor"));

            return strLevel + "\n" + strSlot + "\n";
        }

        protected string CreateModifiersText()
        {
            //TODO:! USARE RISORSE
            string strModifiers = "";
            if (modifiers != null && modifiers.Length > 0)
            {
                strModifiers += "Modificatori:\n";
                foreach (StatModifier modifier in modifiers)
                {
                    //NOME STAT
                    //TODO DA FARE
                    //VALORE
                    if (modifier.BonusValue > 0)
                        strModifiers += "+";
                    else
                        strModifiers += "-";
                    strModifiers += modifier.BonusValue;
                    //SE è %
                    if (modifier.StatType == StatModifierType.Percent)
                        strModifiers += "%";
                    //NUOVA RIGA
                    strModifiers += "\n";
                }
                //RIMUOVO IL \n FINALE
                strModifiers = strModifiers.Substring(0, strModifiers.Length - 1).Trim();
            }
            return strModifiers;
        }
    }
}