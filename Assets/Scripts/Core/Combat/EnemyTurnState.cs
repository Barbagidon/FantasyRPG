using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние хода вражеского юнита под управлением ИИ.
    /// </summary>
    public class EnemyTurnState : ICombatState
    {
        private readonly Hero _enemyUnit;

        public int CurrentAP { get; private set; }

        public EnemyTurnState(Hero enemyUnit)
        {
            _enemyUnit = enemyUnit;
        }

        public void Enter()
        {
            CurrentAP = _enemyUnit.Stats.MaxActionPoints;
        }

        public void Exit() { }

        public bool TrySpendAP(int apCost)
        {
            if (CurrentAP >= apCost)
            {
                CurrentAP = CurrentAP - apCost;
                return true;
            }

            return false;
        }
    }
}
