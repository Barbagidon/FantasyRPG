using System;

namespace FantasyRPG.Core.Stats
{
    public static class DamageCalculator
    {
        private static Random _rng = new();

        public static void SetSeed(int seed)
        {
            _rng = new(seed);
        }

        public static int CalculateDamage(Hero attacker, Hero defender)
        {
            int weaponDamage =
                attacker.EquippedWeapon != null ? attacker.EquippedWeapon.BaseDamage : 0;
            int totalAttack = attacker.Stats.BaseAttack + weaponDamage;
            int armorDefense = defender.EquippedArmor != null ? defender.EquippedArmor.Defense : 0;
            int totalDefense = defender.Stats.BaseDefense + armorDefense;
            int baseDamage = Math.Max(1, totalAttack - totalDefense);
            bool isCritical = _rng.NextDouble() <= attacker.Stats.CritChance;

            if (isCritical)
            {
                return (int)(baseDamage * attacker.Stats.CritMultiplier);
            }
            return baseDamage;
        }
    }
}

