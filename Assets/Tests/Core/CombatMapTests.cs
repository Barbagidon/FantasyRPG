using System;
using FantasyRPG.Core.Combat;
using NUnit.Framework;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class CombatMapTests
    {
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(0, 5)]
        [TestCase(5, 0)]
        public void IsValid_PositionOutsideBounds_ReturnsFalse(int testX, int testY)
        {
            CombatMap map = new(5, 5);
            GridPosition position = new(testX, testY);
            Assert.IsFalse(map.IsValid(position));
        }

        [TestCase(0, 0)]
        [TestCase(4, 4)]
        public void IsValid_PositionInsideBounds_ReturnsTrue(int testX, int testY)
        {
            CombatMap map = new(5, 5);
            GridPosition position = new(testX, testY);
            Assert.IsTrue(map.IsValid(position));
        }

        [TestCase(0, 0, 0)]
        [TestCase(7, 2, 1)]
        [TestCase(4, 4, 0)]
        [TestCase(5, 0, 1)]
        public void FromIndex_ValidIndex_ReturnsExpectedCoordinates(
            int index,
            int expectedX,
            int expectedY
        )
        {
            CombatMap map = new(5, 5);

            GridPosition result = map.FromIndex(index);

            Assert.AreEqual(new GridPosition(expectedX, expectedY), result);
        }

        [TestCase(-1)]
        [TestCase(25)]
        [TestCase(100)]
        public void FromIndex_IndexOutOfRange_ThrowsArgumentOutOfRangeException(int index)
        {
            CombatMap map = new(5, 5);

            Assert.Throws<ArgumentOutOfRangeException>(() => map.FromIndex(index));
        }

        [TestCase(0, 0, 0)]
        [TestCase(2, 1, 7)]
        [TestCase(4, 0, 4)]
        [TestCase(0, 1, 5)]
        public void ToIndex_ValidPosition_ReturnsExpectedIndex(
            int testX,
            int testY,
            int expectedIndex
        )
        {
            CombatMap map = new(5, 5);
            GridPosition position = new(testX, testY);

            Assert.AreEqual(expectedIndex, map.ToIndex(position));
        }

        [TestCase(-1, 0)]
        [TestCase(5, 0)]
        [TestCase(0, 5)]
        [TestCase(0, -1)]
        public void ToIndex_PositionOutsideBounds_ThrowsArgumentOutOfRangeException(
            int testX,
            int testY
        )
        {
            CombatMap map = new(5, 5);
            GridPosition position = new(testX, testY);

            Assert.Throws<ArgumentOutOfRangeException>(() => map.ToIndex(position));
        }

        [TestCase(-1, 1)]
        [TestCase(0, 1)]
        public void Constructor_WidthZeroOrNegative_ThrowsArgumentOutOfRangeException(
            int testWidth,
            int testHeight
        )
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombatMap(testWidth, testHeight));
        }

        [TestCase(1, -1)]
        [TestCase(1, 0)]
        public void Constructor_HeightZeroOrNegative_ThrowsArgumentOutOfRangeException(
            int testWidth,
            int testHeight
        )
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CombatMap(testWidth, testHeight));
        }
    }
}
