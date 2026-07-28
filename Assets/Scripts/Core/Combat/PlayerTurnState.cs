using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние хода конкретного персонажа игрока. Управляет списанием Action Points (AP).
    /// </summary>
    public class PlayerTurnState : ICombatState
    {
        private readonly CombatStateMachine _stateMachine;
        private readonly Hero _activeHero;

        public int CurrentAP { get; private set; }

        public PlayerTurnState(CombatStateMachine stateMachine, Hero activeHero)
        {
            _stateMachine = stateMachine;
            _activeHero = activeHero;
        }

        public bool TrySpendAP(int apCost)
        {
            if (CurrentAP >= apCost)
            {
                CurrentAP = CurrentAP - apCost;
                return true;
            }

            return false;
        }

        public void Enter()
        {
            CurrentAP = _activeHero.Stats.MaxActionPoints;
        }

        public void Exit() { }
    }
}


