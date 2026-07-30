using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние хода вражеского юнита под управлением ИИ.
    /// </summary>
    public class EnemyTurnState : ICombatState
    {
        private readonly Hero _enemyUnit;

        public EnemyTurnState(Hero enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }

        public void Enter()
        {
            _enemyUnit.ResetActionPoints();
        }

        public void Exit() { }
    }
}
