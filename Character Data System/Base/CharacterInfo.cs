using System;
using UnityEngine;

namespace CharacterDataSystem
{
    [Serializable]
    public class CharacterInfo
    {
        [SerializeField]
        private string characterName = "";
        public string CharacterName => characterName;
        [SerializeField]
        private CharacterRace characterRace = CharacterRace.Hero;
        public CharacterRace CharacterRace => characterRace;

        public CharacterInfo(string characterName, CharacterRace characterRace)
        {
            this.characterName = characterName;
            this.characterRace = characterRace;
        }
    }
}