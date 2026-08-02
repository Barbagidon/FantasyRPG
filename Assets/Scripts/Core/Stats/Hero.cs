using System;
using FantasyRPG.Core.Items;

namespace FantasyRPG.Core.Stats
{
    public class Hero
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int CurrentActionPoints { get; private set; }
        public HeroStats Stats { get; private set; }
        public Weapon EquippedWeapon { get; private set; }
        public Armor EquippedArmor { get; private set; }
        public int CurrentHealth { get; private set; }

        public Hero(int id, string name, HeroStats baseStats)
        {
            Id = id;
            Name = name;
            CurrentActionPoints = baseStats.MaxActionPoints;
            Stats = baseStats;
            CurrentHealth = baseStats.MaxHealth;
        }

        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;
        }

        public bool TrySpendAP(int cost)
        {
            if (CurrentActionPoints >= cost)
            {
                CurrentActionPoints -= cost;
                return true;
            }
            return false;
        }

        public void ResetActionPoints()
        {
            CurrentActionPoints = Stats.MaxActionPoints;
        }

        public void EquipArmor(Armor armor)
        {
            EquippedArmor = armor;
        }

        public void TakeDamage(int damageAmount)
        {
            CurrentHealth = Math.Max(0, CurrentHealth - damageAmount);
        }
    }
}

