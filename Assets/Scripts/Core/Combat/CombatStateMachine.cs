namespace FantasyRPG.Core.Combat
{
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
