namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Контракт для всех состояний боевого автомата (FSM).
    /// </summary>
    public interface ICombatState
    {
        void Enter();
        void Exit();
    }
}


