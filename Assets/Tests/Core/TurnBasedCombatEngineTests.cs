using System.Collections.Generic;
using FantasyRPG.Core.Combat;
using FantasyRPG.Core.Stats;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class TurnBasedCombatEngineTests
    {
        [Test]
        public void AdvanceTurn_SingleAliveUnitEachSide_SwitchesActiveUnitToEnemy()
        {
            HeroStats attackerStats = new HeroStats(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 10,
                baseDefense: 0,
                speed: 5,
                critChance: 1f,
                critMultiplier: 2f
            );
            HeroStats defenderStats = new HeroStats(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 0,
                baseDefense: 0,
                speed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );

            Hero playerHero = new Hero("Attacker", attackerStats);
            Hero enemyHero = new Hero("Defender", defenderStats);
            List<Hero> playerTeam = new List<Hero> { playerHero };
            List<Hero> enemyTeam = new List<Hero> { enemyHero };

            TurnBasedCombatEngine engine = new TurnBasedCombatEngine(playerTeam, enemyTeam);
            engine.StartCombat();
            engine.AdvanceTurn();
            ClassicAssert.AreEqual(enemyHero, engine.ActiveUnit);
        }

        [Test]
        public void CheckCombatEnd_AllEnemiesDead_ReturnsTrueAndSetsVictoryState()
        {
            const int enemyHealth = 20;
            const int enemyDamage = enemyHealth + 1;

            HeroStats attackerStats = new HeroStats(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 10,
                baseDefense: 0,
                speed: 5,
                critChance: 1f,
                critMultiplier: 2f
            );
            HeroStats defenderStats = new HeroStats(
                maxHealth: enemyHealth,
                maxActionPoints: 10,
                baseAttack: 0,
                baseDefense: 0,
                speed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );

            Hero playerHero = new Hero("Attacker", attackerStats);
            Hero enemyHero = new Hero("Defender", defenderStats);
            List<Hero> playerTeam = new List<Hero> { playerHero };
            List<Hero> enemyTeam = new List<Hero> { enemyHero };

            TurnBasedCombatEngine engine = new TurnBasedCombatEngine(playerTeam, enemyTeam);
            enemyHero.TakeDamage(enemyDamage);
            bool didCombatEnd = engine.CheckCombatEnd();
            ClassicAssert.IsTrue(didCombatEnd);
            ClassicAssert.IsInstanceOf<VictoryState>(engine.StateMachine.CurrentState);
        }

        [Test]
        public void CheckCombatEnd_AllPlayersDead_ReturnsTrueAndSetsDefeatState()
        {
            const int playerHealth = 20;
            const int playerDamage = playerHealth + 1;

            HeroStats attackerStats = new HeroStats(
                maxHealth: playerHealth,
                maxActionPoints: 10,
                baseAttack: 10,
                baseDefense: 0,
                speed: 5,
                critChance: 1f,
                critMultiplier: 2f
            );
            HeroStats defenderStats = new HeroStats(
                maxHealth: 20,
                maxActionPoints: 10,
                baseAttack: 0,
                baseDefense: 0,
                speed: 5,
                critChance: 0f,
                critMultiplier: 2f
            );

            Hero playerHero = new Hero("Attacker", attackerStats);
            Hero enemyHero = new Hero("Defender", defenderStats);
            List<Hero> playerTeam = new List<Hero> { playerHero };
            List<Hero> enemyTeam = new List<Hero> { enemyHero };

            TurnBasedCombatEngine engine = new TurnBasedCombatEngine(playerTeam, enemyTeam);
            playerHero.TakeDamage(playerDamage);
            bool didCombatEnd = engine.CheckCombatEnd();
            ClassicAssert.IsTrue(didCombatEnd);
            ClassicAssert.IsInstanceOf<DefeatState>(engine.StateMachine.CurrentState);
        }
    }
}

