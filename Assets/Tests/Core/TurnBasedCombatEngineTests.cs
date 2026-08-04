using System.Collections.Generic;
using FantasyRPG.Core.Combat;
using FantasyRPG.Core.Stats;
using NUnit.Framework;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class TurnBasedCombatEngineTests
    {
        [Test]
        public void AdvanceTurn_SingleAliveUnitEachSide_SwitchesActiveUnitToEnemy()
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

            Hero playerHero = new(0, "Attacker", attackerStats, ownerId: 0);
            Hero enemyHero = new(1, "Defender", defenderStats, ownerId: 0);
            List<Hero> playerTeam = new() { playerHero };
            List<Hero> enemyTeam = new() { enemyHero };

            TurnBasedCombatEngine engine = new(playerTeam, enemyTeam);
            engine.StartCombat();
            engine.AdvanceTurn();
            Assert.AreEqual(enemyHero, engine.ActiveUnit);
        }

        [Test]
        public void CheckCombatEnd_AllEnemiesDead_ReturnsTrueAndSetsVictoryState()
        {
            const int enemyHealth = 20;
            const int enemyDamage = enemyHealth + 1;

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
                maxHealth: enemyHealth,
                maxActionPoints: 10,
                baseAttack: 0,
                baseDefense: 0,
                initiative: 5,
                moveSpeed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );

            Hero playerHero = new(0, "Attacker", attackerStats, ownerId: 0);
            Hero enemyHero = new(1, "Defender", defenderStats, ownerId: 0);
            List<Hero> playerTeam = new() { playerHero };
            List<Hero> enemyTeam = new() { enemyHero };

            TurnBasedCombatEngine engine = new(playerTeam, enemyTeam);
            enemyHero.TakeDamage(enemyDamage);
            bool didCombatEnd = engine.CheckCombatEnd();
            Assert.IsTrue(didCombatEnd);
            Assert.IsInstanceOf<VictoryState>(engine.StateMachine.CurrentState);
        }

        [Test]
        public void CheckCombatEnd_AllPlayersDead_ReturnsTrueAndSetsDefeatState()
        {
            const int playerHealth = 20;
            const int playerDamage = playerHealth + 1;

            HeroStats attackerStats = new(
                maxHealth: playerHealth,
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

            Hero playerHero = new(0, "Attacker", attackerStats, ownerId: 0);
            Hero enemyHero = new(1, "Defender", defenderStats, ownerId: 0);
            List<Hero> playerTeam = new() { playerHero };
            List<Hero> enemyTeam = new() { enemyHero };

            TurnBasedCombatEngine engine = new(playerTeam, enemyTeam);
            playerHero.TakeDamage(playerDamage);
            bool didCombatEnd = engine.CheckCombatEnd();
            Assert.IsTrue(didCombatEnd);
            Assert.IsInstanceOf<DefeatState>(engine.StateMachine.CurrentState);
        }

        [Test]
        public void GetUnitById_ExistingId_ReturnsMatchingHero()
        {
            const int playerId = 0;
            const int enemyId = 1;

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

            Hero playerHero = new(playerId, "Attacker", attackerStats, ownerId: 0);
            Hero enemyHero = new(enemyId, "Defender", defenderStats, ownerId: 0);
            List<Hero> playerTeam = new() { playerHero };
            List<Hero> enemyTeam = new() { enemyHero };

            TurnBasedCombatEngine engine = new(playerTeam, enemyTeam);
            Assert.AreEqual(engine.GetUnitById(playerId), playerHero);
        }

        [Test]
        public void GetUnitById_UnknownId_ReturnsNull()
        {
            const int playerId = 0;
            const int enemyId = 1;
            const int wrongId = 2;

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

            Hero playerHero = new(playerId, "Attacker", attackerStats, ownerId: 0);
            Hero enemyHero = new(enemyId, "Defender", defenderStats, ownerId: 0);
            List<Hero> playerTeam = new() { playerHero };
            List<Hero> enemyTeam = new() { enemyHero };

            TurnBasedCombatEngine engine = new(playerTeam, enemyTeam);
            Assert.IsNull(engine.GetUnitById(wrongId));
        }
    }
}

