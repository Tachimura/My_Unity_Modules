using System;
using UnityEngine;

namespace CharacterDataSystem
{
    namespace Player
    {
        [Serializable]
        public struct ExpLevelLine
        {
            [Header("Character Stats")]
            public int baseHP;
            public int baseMP;
            public int baseAttack;
            public int baseDefense;
            public int baseCriticalRate;
            public int baseCriticalDamage;

            [Header("Player Stats")]
            public int baseStamina;
            public int expNextLevel;
        }
    }
}