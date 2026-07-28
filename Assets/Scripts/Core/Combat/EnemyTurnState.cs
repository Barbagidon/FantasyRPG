namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние хода вражеского юнита под управлением ИИ.
    /// </summary>
    public class EnemyTurnState : ICombatState
    {
        private readonly CombatStateMachine _stateMachine;
        private readonly Hero _enemyUnit;

        public int CurrentAP { get; private set; }

        public EnemyTurnState(CombatStateMachine stateMachine, Hero enemyUnit)
        {
            _stateMachine = stateMachine;
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


