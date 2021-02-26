using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace CharacterDataSystem
{
    [Serializable]
    public class CharacterStat
    {
        [SerializeField]
        private int statBaseValue = 0;
        public int BaseValue
        {
            get => statBaseValue;
            set
            {
                isDirty = true;
                statBaseValue = value;
            }
        }
        private int statValue = 0;
        public int Value
        {
            get
            {
                if (isDirty)
                {
                    statValue = CalculateValue();
                    isDirty = false;
                }
                return statValue;
            }

            protected set { statValue = value; }
        }
        private bool isDirty = true;

        private readonly List<StatModifier> statFlatModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatFlatModifiers;
        private readonly List<StatModifier> statPercentModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatPercentModifiers;
        public CharacterStat()
        {
            statFlatModifiers = new List<StatModifier>();
            StatFlatModifiers = statFlatModifiers.AsReadOnly();
            statPercentModifiers = new List<StatModifier>();
            StatPercentModifiers = statPercentModifiers.AsReadOnly();
        }
        public CharacterStat(int baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            switch (modifier.StatType)
            {
                case StatModifierType.Flat:
                    {
                        statFlatModifiers.Add(modifier);
                        break;
                    }
                case StatModifierType.Percent:
                    {
                        statPercentModifiers.Add(modifier);
                        break;
                    }
            }
            isDirty = true;
        }
        public bool RemoveModifier(StatModifier modifier)
        {
            bool removed = false;
            switch (modifier.StatType)
            {
                case StatModifierType.Flat:
                    {
                        removed = statFlatModifiers.Remove(modifier);
                        break;
                    }
                case StatModifierType.Percent:
                    {
                        removed = statPercentModifiers.Remove(modifier);
                        break;
                    }
            }
            isDirty = isDirty || removed;
            /*
            if (!isDirty)
                isDirty = removed;
            */
            return removed;
        }
        public bool RemoveAllModifiersFromSource(object source)
        {
            bool somethingRemoved = statFlatModifiers.RemoveAll(modifier => modifier.Source == source) > 0;
            somethingRemoved = statPercentModifiers.RemoveAll(modifier => modifier.Source == source) > 0 || somethingRemoved;
            isDirty = isDirty || somethingRemoved;
            /*
            if (somethingRemoved)
                isDirty = true;
            */
            return somethingRemoved;
        }

        private int CalculateValue()
        {
            float flatValue = statBaseValue;
            float percentValue = 0;
            //FLAT VALUE
            foreach (StatModifier modifier in statFlatModifiers)
                flatValue += modifier.BonusValue;
            //PERCENT VALUE
            foreach (StatModifier modifier in statPercentModifiers)
                percentValue += modifier.BonusValue;
            //RITONO IL FLAT VALUE + IL PERCENT VALUE
            //DIVIDO PER 100 PERCHè MI SERVE LA PERCENTUALE
            return (int)(flatValue + (flatValue * (percentValue / 100)));
        }
    }
}