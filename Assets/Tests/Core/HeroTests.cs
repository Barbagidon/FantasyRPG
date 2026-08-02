using FantasyRPG.Core.Stats;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class HeroTests
    {
        [Test]
        public void TrySpendAP_CostExceedsCurrentAP_ReturnsFalseAndDoesNotSpend()
        {
            const int maxActionPoints = 5;

            HeroStats stats = new(
                maxHealth: 20,
                maxActionPoints: maxActionPoints,
                baseAttack: 10,
                baseDefense: 0,
                speed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );

            Hero hero = new("Attacker", stats);
            bool result = hero.TrySpendAP(6);

            ClassicAssert.IsFalse(result);
            ClassicAssert.AreEqual(maxActionPoints, hero.CurrentActionPoints);
        }

        [Test]
        public void TrySpendAP_CostWithinCurrentAP_ReturnsTrueAndSpendsCost()
        {
            const int maxActionPoints = 5;
            const int cost = 3;

            HeroStats stats = new(
                maxHealth: 20,
                maxActionPoints: maxActionPoints,
                baseAttack: 10,
                baseDefense: 0,
                speed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );
            Hero hero = new("Attacker", stats);

            bool result = hero.TrySpendAP(cost);

            ClassicAssert.IsTrue(result);
            ClassicAssert.AreEqual(maxActionPoints - cost, hero.CurrentActionPoints);
        }

        [Test]
        public void TakeDamage_LessThanCurrentHealth_ReducesHealthByAmount()
        {
            const int maxHealth = 20;
            const int damage = 8;

            HeroStats stats = new(
                maxHealth: maxHealth,
                maxActionPoints: 5,
                baseAttack: 10,
                baseDefense: 0,
                speed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );
            Hero hero = new("Defender", stats);

            hero.TakeDamage(damage);

            ClassicAssert.AreEqual(maxHealth - damage, hero.CurrentHealth);
        }

        [Test]
        public void TakeDamage_ExceedsCurrentHealth_ClampsHealthToZero()
        {
            const int maxHealth = 20;
            const int lethalDamage = 999;

            HeroStats stats = new(
                maxHealth: maxHealth,
                maxActionPoints: 5,
                baseAttack: 10,
                baseDefense: 0,
                speed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );
            Hero hero = new("Defender", stats);

            hero.TakeDamage(lethalDamage);

            ClassicAssert.AreEqual(0, hero.CurrentHealth);
        }
    }
}

