namespace CharacterDataSystem
{
    public class CharacterStats
    {
        protected int level = 0;
        public int Level
        {
            get => level;
            set
            {
                if (value < 0)
                    value = 0;
                level = value;
            }
        }

        protected int hp = 0;
        public int HP
        {
            get => hp;
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > MaxHP.Value)
                    value = MaxHP.Value;
                hp = value;
            }
        }
        public CharacterStat MaxHP { get; private set; } = null;

        protected int mp = 0;
        public int MP
        {
            get => mp;
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > MaxMP.Value)
                    value = MaxMP.Value;
                mp = value;
            }
        }
        public CharacterStat MaxMP { get; private set; } = null;

        public CharacterStat Attack { get; private set; } = null;
        public CharacterStat Defense { get; private set; } = null;
        public CharacterStat CriticalRate { get; private set; } = null;
        public CharacterStat CriticalDamage { get; private set; } = null;

        //TODO //LISTA DI MODIFICATORI
        /*
        private List<CharacterStatModifier> attackModifiers = new List<CharacterStatModifier>();
        private List<CharacterStatModifier> defenseModifiers = new List<CharacterStatModifier>();
        */

        public CharacterStats()
        {
            Level = 1;

            if (MaxHP == null)
                MaxHP = new CharacterStat(10);
            HP = MaxHP.Value;

            if (MaxMP == null)
                MaxMP = new CharacterStat(10);
            MP = MaxMP.Value;

            if (Attack == null)
                Attack = new CharacterStat(1);

            if (Defense == null)
                Defense = new CharacterStat(1);

            if (CriticalRate == null)
                CriticalRate = new CharacterStat(5);

            if (CriticalDamage == null)
                CriticalDamage = new CharacterStat(50);
        }

        public CharacterStats(int level, int hp, int maxHP, int mp, int maxMP, int attack, int defense, int criticalRate, int criticalDamage)
        {

            Level = level;

            if (MaxHP == null)
                MaxHP = new CharacterStat(maxHP);
            HP = hp;

            if (MaxMP == null)
                MaxMP = new CharacterStat(maxMP);
            MP = mp;

            if (Attack == null)
                Attack = new CharacterStat(attack);

            if (Defense == null)
                Defense = new CharacterStat(defense);

            if (CriticalRate == null)
                CriticalRate = new CharacterStat(criticalRate);

            if (CriticalDamage == null)
                CriticalDamage = new CharacterStat(criticalDamage);
        }
    }
}