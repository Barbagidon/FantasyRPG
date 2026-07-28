namespace FantasyRPG.Core.Stats
{
    public readonly struct HeroStats
    {
        public int MaxHealth { get; }
        public int CurrentHealth { get; }
        public int MaxActionPoints { get; }
        public int BaseAttack { get; }
        public int BaseDefense { get; }
        public int Speed { get; }
        public float CritChance { get; }
        public float CritMultiplier { get; }

        public HeroStats(
            int maxHealth,
            int currentHealth,
            int maxActionPoints,
            int baseAttack,
            int baseDefense,
            int speed,
            float critChance,
            float critMultiplier
        )
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            MaxActionPoints = maxActionPoints;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            Speed = speed;
            CritChance = critChance;
            CritMultiplier = critMultiplier;
        }
    }
}
