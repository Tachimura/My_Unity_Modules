using System;
using UnityEngine;
using UtilityInterface;

namespace InventorySystem.Items
{
    [Serializable]
    public abstract class Item : ScriptableObject, IDescribable
    {
        //IDENTIFICATIVO UNIVOCO DELL'OGGETTO
        //public string ID { get; } = Guid.NewGuid().ToString();

        //NOME DELL'OGGETTO
        [SerializeField]
        private string itemName = "";
        public string ItemName => itemName;

        //VALORE DELL'OGGETTO, USABILE PER SHOP
        [SerializeField]
        [Min(0)]
        private int itemValue = 0;
        public int ItemValue => itemValue;

        //STACKABILITY DELL'OGGETTO
        [SerializeField]
        private bool isStackable = false;
        public bool IsStackable => isStackable;

        //RARITà DELL'OGGETTO
        [SerializeField]
        private ItemRarity itemRarity = ItemRarity.Common;
        public ItemRarity ItemRarity => ItemRarity;

        //TIPOLOGIA DELL'OGGETTO (EQUIPAGGIAMENTO, RISORSE X CRAFT, CONSUMABILI COME POZIONI, ECC...)
        public ItemType ItemType { get; protected set; } = ItemType.Equipment;

        //ICONA CHE RAPPRESENTA IL NOSTRO OGGETTO
        [SerializeField]
        private Sprite itemIcon = null;
        public Sprite DescribableIcon => itemIcon;

        public abstract string DescribableName { get; protected set; }

        public abstract string DescribableDescription { get; protected set; }

        //METODO PER INDICARE L'UGUAGLIANZA TRA DUE ITEMS
        public bool EqualsTo(Item item)
        {
            //SE L'ITEM IN INGRESSO è NULLO RITORNO FALSO
            if (item == null)
                return false;
            //ALTRIMENTI CONTROLLO I DUE NOMI
            return ItemName.CompareTo(item.ItemName) == 0;
        }
    }
}