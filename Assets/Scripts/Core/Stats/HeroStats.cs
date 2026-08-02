namespace FantasyRPG.Core.Stats
{
    public readonly struct HeroStats
    {
        public int MaxHealth { get; }
        public int MaxActionPoints { get; }
        public int BaseAttack { get; }
        public int BaseDefense { get; }
        public int Initiative { get; }

        // Потребитель — MoveCommand (Веха 1), сейчас не используется.
        public int MoveSpeed { get; }
        public float CritChance { get; }
        public float CritMultiplier { get; }

        public HeroStats(
            int maxHealth,
            int maxActionPoints,
            int baseAttack,
            int baseDefense,
            int initiative,
            int moveSpeed,
            float critChance,
            float critMultiplier
        )
        {
            MaxHealth = maxHealth;
            MaxActionPoints = maxActionPoints;
            BaseAttack = baseAttack;
            BaseDefense = baseDefense;
            Initiative = initiative;
            MoveSpeed = moveSpeed;
            CritChance = critChance;
            CritMultiplier = critMultiplier;
        }
    }
}

