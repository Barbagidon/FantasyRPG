using System;

namespace FantasyRPG.Core.Combat
{
    public class CombatMap
    {
        public int Width { get; }
        public int Height { get; }

        public CombatMap(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(width),
                    width,
                    "Width must be greater than zero."
                );
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(height),
                    height,
                    "Height must be greater than zero."
                );
            }

            Width = width;
            Height = height;
        }

        public bool IsValid(GridPosition position)
        {
            return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
        }

        public GridPosition FromIndex(int index)
        {
            if (index < 0 || index >= Width * Height)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    index,
                    "Index must be within [0, Width * Height)."
                );
            }

            int x = index % Width;
            int y = index / Width;

            return new GridPosition(x, y);
        }

        public int ToIndex(GridPosition position)
        {
            if (!IsValid(position))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(position),
                    position,
                    "Position must be within map bounds."
                );
            }

            int index = position.Y * Width + position.X;

            return index;
        }
    }
}
