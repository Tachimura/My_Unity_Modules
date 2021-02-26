namespace CharacterDataSystem
{
    public class CharacterStatModifier
    {
        public CharacterStat CharacterStat { get; private set; }
        public StatModifier StatModifier { get; private set; }

        public CharacterStatModifier(CharacterStat characterStat, StatModifier statModifier)
        {
            CharacterStat = characterStat;
            StatModifier = statModifier;
        }
    }
}