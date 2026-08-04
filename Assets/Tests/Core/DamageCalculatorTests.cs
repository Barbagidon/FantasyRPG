using FantasyRPG.Core.Stats;
using NUnit.Framework;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class DamageCalculatorTests
    {
        [Test]
        public void CalculateDamage_NoCrit_ReturnsAttackMinusDefense()
        {
            HeroStats attackerStats = new(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 10,
                baseDefense: 0,
                initiative: 5,
                moveSpeed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );
            HeroStats defenderStats = new(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 0,
                baseDefense: 0,
                initiative: 5,
                moveSpeed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );

            Hero attacker = new(0, "Attacker", attackerStats, ownerId: 0);
            Hero defender = new(1, "Defender", defenderStats, ownerId: 0);

            int actual = DamageCalculator.CalculateDamage(attacker, defender);

            Assert.AreEqual(10, actual);
        }

        [Test]
        public void CalculateDamage_GuaranteedCrit_ReturnsBaseDamageTimesCritMultiplier()
        {
            HeroStats attackerStats = new(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 10,
                baseDefense: 0,
                initiative: 5,
                moveSpeed: 5,
                critChance: 1f,
                critMultiplier: 2f
            );
            HeroStats defenderStats = new(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 0,
                baseDefense: 0,
                initiative: 5,
                moveSpeed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );

            Hero attacker = new(0, "Attacker", attackerStats, ownerId: 0);
            Hero defender = new(1, "Defender", defenderStats, ownerId: 0);

            int actual = DamageCalculator.CalculateDamage(attacker, defender);

            Assert.AreEqual(20, actual);
        }
    }
}

