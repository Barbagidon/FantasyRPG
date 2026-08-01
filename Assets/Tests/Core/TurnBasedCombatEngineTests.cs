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
    }
}

