namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Контекст и дирижер конечного автомата состояний боя (Turn-Based FSM).
    /// </summary>
    public class CombatStateMachine
    {
        public ICombatState CurrentState { get; private set; }

        public void ChangeState(ICombatState newState)
        {
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = newState;

            if (CurrentState != null)
                CurrentState.Enter();
        }
    }
}


