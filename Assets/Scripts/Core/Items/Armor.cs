namespace FantasyRPG.Core.Items
{
    public class Armor
    {
        public string Name { get; private set; }
        public ArmorType Type { get; private set; }
        public int Defense { get; private set; }

        public Armor(string name, ArmorType type, int defense)
        {
            Name = name;
            Type = type;
            Defense = defense;
        }
    }
}
