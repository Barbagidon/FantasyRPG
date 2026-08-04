namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние поражения игроков в бою.
    /// </summary>
    public class DefeatState : ICombatState
    {
        // No cleanup required for terminal state.
        public void Enter() { }

        public void Exit() { }
    }
}
