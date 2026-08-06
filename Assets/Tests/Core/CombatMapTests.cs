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
    }
}
