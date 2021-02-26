using System;
using UnityEngine;

namespace CharacterDataSystem
{
    [Serializable]
    public class StatModifier
    {
        //IL VALORE BONUS CHE OFFRE (POSITIVO O NEGATIVO) QUESTO STAT MODIFIER
        [SerializeField]
        private int bonusValue = 0;
        public int BonusValue { get => bonusValue; }

        //LA TIPOLOGIA DI QUESTA CHARACTER STAT MODIFIER, SE FLAT O PERCENTUALE
        [SerializeField]
        private StatModifierType statType = StatModifierType.Flat;
        public StatModifierType StatType { get => statType; }

        //LA SORGENTE DI QUESTA STAT MODIFIER
        private object source = null;
        public object Source { get => source; set { if (source == null) source = value; } }
    }
}