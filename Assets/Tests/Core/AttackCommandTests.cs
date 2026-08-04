using System.Collections.Generic;
using FantasyRPG.Core.Combat;
using FantasyRPG.Core.Stats;
using NUnit.Framework;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class AttackCommandTests
    {
        [Test]
        public void Execute_ValidAttack_DealsDamageAndSpendsAP()
        {
            const int maxActionPoints = 10;
            const int baseAttack = 10;
            const int maxHealth = 20;
            const int apCost = maxActionPoints - 5;

            HeroStats attackerStats = new(
                maxHealth: 20,
                maxActionPoints,
                baseAttack,
                baseDefense: 0,
                initiative: 5,
                moveSpeed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );
            HeroStats defenderStats = new(
                maxHealth,
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

            List<Hero> playerTeam = new() { attacker };
            List<Hero> enemyTeam = new() { defender };

            TurnBasedCombatEngine engine = new(playerTeam, enemyTeam);
            AttackCommand cmd = new(attacker.Id, defender.Id, apCost, engine);
            bool result = cmd.Execute();
            Assert.IsTrue(result);
            Assert.AreEqual(maxHealth - baseAttack, defender.CurrentHealth);
            Assert.AreEqual(maxActionPoints - (apCost), attacker.CurrentActionPoints);
        }

        [Test]
        public void CanExecute_InsufficientAP_ReturnsFalse()
        {
            const int maxActionPoints = 10;
            const int apCost = maxActionPoints + 5;

            HeroStats attackerStats = new(
                maxHealth: 20,
                maxActionPoints,
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

            List<Hero> playerTeam = new() { attacker };
            List<Hero> enemyTeam = new() { defender };

            TurnBasedCombatEngine engine = new(playerTeam, enemyTeam);
            AttackCommand cmd = new(attacker.Id, defender.Id, apCost, engine);
            bool result = cmd.CanExecute();
            Assert.IsFalse(result);
        }
    }
}

