namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние поражения игроков в бою.
    /// </summary>
    public class DefeatState : ICombatState
    {
        private readonly CombatStateMachine _stateMachine;

        public DefeatState(CombatStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter() { }

        public void Exit() { }
    }
}

