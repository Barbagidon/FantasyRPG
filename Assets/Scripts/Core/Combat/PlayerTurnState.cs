using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние хода конкретного персонажа игрока. Управляет списанием Action Points (AP).
    /// </summary>
    public class PlayerTurnState : ICombatState
    {
        private readonly Hero _activeHero;

        public PlayerTurnState(Hero activeHero)
        {
            _activeHero = activeHero;
        }

        public void Enter()
        {
            _activeHero.ResetActionPoints();
        }

        public void Exit() { }
    }
}
