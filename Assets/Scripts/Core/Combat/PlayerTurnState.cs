using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    public class PlayerTurnState : ICombatState
    {
        private readonly CombatStateMachine _stateMachine;
        private readonly Hero _activeHero;
        public readonly int CurrentAp { get; private set; }

        public PlayerTurnState(CombatStateMachine stateMachine, Hero activeHero)
        {
            _stateMachine = stateMachine;
            _activeHero = activeHero;
        }

        public void Enter()
        {
            CurrentAp = _activeHero.Stats.MaxActionPoints;
        }

        public void Exit() { }
    }
}
