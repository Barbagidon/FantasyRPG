namespace FantasyRPG.Core.Items
{
    public class Weapon
    {
        public string Name { get; private set; }
        public WeaponType Type { get; private set; }
        public int BaseDamage { get; private set; }

        public Weapon(string name, WeaponType type, int baseDamage)
        {
            Name = name;
            Type = type;
            BaseDamage = baseDamage;
        }
    }
}
