using FantasyRPG.Core.Items;

namespace FantasyRPG.Core.Stats
{
    public class Hero
    {
        public string Name { get; private set; }
        public HeroStats Stats { get; private set; }
        public Weapon EquippedWeapon { get; private set; }
        public Armor EquippedArmor { get; private set; }

        public Hero(string name, HeroStats baseStats)
        {
            Name = name;
            Stats = baseStats;
            EquippedWeapon = null;
            EquippedArmor = null;
        }

        public void EquipWeapon(Weapon weapon)
        {
            EquippedWeapon = weapon;
        }

        public void EquipArmor(Armor armor)
        {
            EquippedArmor = armor;
        }
    }
}
