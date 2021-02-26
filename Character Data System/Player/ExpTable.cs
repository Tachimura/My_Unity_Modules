using System;
using System.Linq;
using UnityEngine;

namespace CharacterDataSystem
{
    namespace Player
    {
        [Serializable]
        [CreateAssetMenu(fileName = "PlayerExpTable", menuName = "ExperienceTable")]
        public class ExpTable : ScriptableObject
        {
            [SerializeField]
            private ExpLevelLine[] levels;

            public ExpLevelLine? GetLevelLine(int playerLevel)
            {
                int position = playerLevel - 1;
                if (position >= 0 && position < levels.Length)
                {
                    return levels[position];
                }
                return null;
            }

            public int[] ExpThresholds() => levels.Select(level => level.expNextLevel).ToArray();

            public bool CanLevelUp(int playerLevel) => playerLevel < levels.Length;

            //CHECKUP CHE LA LISTA ABBIA ALMENO 1 LIVELLO, E NON OLTRE I 99
            private void OnValidate()
            {
                if (levels.Length > 99)
                    Array.Resize(ref levels, 99);
                else if (levels.Length == 0)
                    Array.Resize(ref levels, 1);
            }
        }
    }
}