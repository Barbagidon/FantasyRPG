namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние победы игроков в бою.
    /// </summary>
    public class VictoryState : ICombatState
    {
        // No cleanup required for terminal state.
        public void Enter() { }

        public void Exit() { }
    }
}
