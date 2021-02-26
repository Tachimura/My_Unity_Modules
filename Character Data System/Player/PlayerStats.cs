namespace CharacterDataSystem
{
    namespace Player
    {
        public class PlayerStats : CharacterStats
        {
            public int Exp { get; set; } = 0;
            public int MaxExp { get; set; } = 1;

            //GESTIONE STAMINA
            private int stamina = 0;
            public int Stamina
            {
                get => stamina;
                set
                {
                    if (value < 0)
                        value = 0;
                    else if (value > MaxStamina.Value)
                        value = MaxStamina.Value;
                    stamina = value;
                }
            }
            public CharacterStat MaxStamina { get; private set; } = null;

            public PlayerStats() : base() => MaxExp = 10;

            public PlayerStats(
                int level, int hp, int maxHP, int mp, int maxMP, int attack, int defense, int criticalRate, int criticalDamage,
                int exp, int maxExp, int stamina, int maxStamina)
                : base(level, hp, maxHP, mp, maxMP, attack, defense, criticalRate, criticalDamage)
            {
                Exp = exp;
                MaxExp = maxExp;
                this.stamina = stamina;
                MaxStamina = new CharacterStat(maxStamina);
            }
            public PlayerStats(
                int level, int maxHP, int maxMP, int attack, int defense, int criticalRate, int criticalDamage,
                int exp, int maxExp, int maxStamina)
                : this(level, maxHP, maxHP, maxMP, maxMP, attack, defense, criticalRate, criticalDamage,
                      exp, maxExp, maxStamina, maxStamina)
            { }

            public void LevelUp(ExpLevelLine levelLine)
            {
                level += 1;
                Exp = 0;
                MaxHP.BaseValue = levelLine.baseHP;
                hp = MaxHP.Value;
                MaxMP.BaseValue = levelLine.baseMP;
                mp = MaxMP.Value;
                Attack.BaseValue = levelLine.baseAttack;
                Defense.BaseValue = levelLine.baseDefense;
                CriticalRate.BaseValue = levelLine.baseCriticalRate;
                CriticalDamage.BaseValue = levelLine.baseCriticalDamage;
                MaxExp = levelLine.expNextLevel;
                MaxStamina.BaseValue = levelLine.baseStamina;
                stamina = MaxStamina.Value;
            }
        }
    }
}