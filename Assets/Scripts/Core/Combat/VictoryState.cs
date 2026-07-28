namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние победы игроков в бою.
    /// </summary>
    public class VictoryState : ICombatState
    {
        private readonly CombatStateMachine _stateMachine;

        public VictoryState(CombatStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter() { }

        public void Exit() { }
    }
}
