using FantasyRPG.Core.Combat;
using NUnit.Framework;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class GridPositionTests
    {
        [Test]
        public void Equals_SameCoordinates_ReturnsTrue()
        {
            GridPosition firstPosition = new(1, 2);
            GridPosition secondPosition = new(1, 2);

            bool areEqual = firstPosition.Equals(secondPosition);
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void Equals_DifferentCoordinates_ReturnsFalse()
        {
            GridPosition firstPosition = new(1, 1);
            GridPosition secondPosition = new(1, 2);

            bool areEqual = firstPosition.Equals(secondPosition);
            Assert.IsFalse(areEqual);
        }

        [Test]
        public void Equals_ObjectOverloadWithSameCoordinates_ReturnsTrue()
        {
            GridPosition firstPosition = new(1, 2);
            GridPosition secondPosition = new(1, 2);

            bool areEqual = firstPosition.Equals((object)secondPosition);
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void EqualsOperator_SameCoordinates_ReturnsTrue()
        {
            GridPosition firstPosition = new(1, 2);
            GridPosition secondPosition = new(1, 2);

            bool areEqual = firstPosition == secondPosition;
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void EqualsOperator_DifferentCoordinates_ReturnsFalse()
        {
            GridPosition firstPosition = new(1, 1);
            GridPosition secondPosition = new(1, 2);

            bool areEqual = firstPosition == secondPosition;
            Assert.IsFalse(areEqual);
        }

        [Test]
        public void NotEqualsOperator_SameCoordinates_ReturnsFalse()
        {
            GridPosition firstPosition = new(1, 1);
            GridPosition secondPosition = new(1, 1);

            bool areEqual = firstPosition != secondPosition;
            Assert.IsFalse(areEqual);
        }

        [Test]
        public void NotEqualsOperator_DifferentCoordinates_ReturnsTrue()
        {
            GridPosition firstPosition = new(1, 2);
            GridPosition secondPosition = new(1, 1);

            bool areEqual = firstPosition != secondPosition;
            Assert.IsTrue(areEqual);
        }

        [Test]
        public void GetHashCode_SwappedCoordinates_ReturnsDifferentHashes()
        {
            GridPosition firstPosition = new(1, 2);
            GridPosition secondPosition = new(2, 1);

            int firstHashCode = firstPosition.GetHashCode();
            int secondHashCode = secondPosition.GetHashCode();

            Assert.AreNotEqual(firstHashCode, secondHashCode);
        }
    }
}
