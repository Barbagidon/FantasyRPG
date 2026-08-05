namespace FantasyRPG.Core.Combat
{
    public class CombatMap
    {
        public int Width { get; }
        public int Height { get; }

        public CombatMap(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsValid(GridPosition pos)
        {
            return pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;
        }

        public GridPosition FromIndex(int index)
        {
            int x = index % Width;
            int y = index / Width;

            return new GridPosition(x, y);
        }
    }
}
