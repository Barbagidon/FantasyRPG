using System.Collections.Generic;
using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Пошаговый движок боя, реализующий логику очередности ходов и смены состояний.
    /// </summary>
    public class TurnBasedCombatEngine
    {
        public CombatStateMachine StateMachine { get; private set; }

        private readonly List<Hero> _playerTeam;

        private readonly List<Hero> _enemyTeam;

        private readonly List<Hero> _turnOrder;

        public int CurrentTurnIndex { get; private set; }

        public Hero ActiveUnit =>
            (_turnOrder != null && _turnOrder.Count > 0) ? _turnOrder[CurrentTurnIndex] : null;

        public TurnBasedCombatEngine(List<Hero> playerTeam, List<Hero> enemyTeam)
        {
            StateMachine = new CombatStateMachine();
            _playerTeam = playerTeam;
            _enemyTeam = enemyTeam;
            CurrentTurnIndex = 0;
            _turnOrder = new List<Hero>();
            _turnOrder.AddRange(_playerTeam);
            _turnOrder.AddRange(_enemyTeam);
        }

        public void StartCombat()
        {
            CurrentTurnIndex = 0;
            InitState initState = new InitState(_turnOrder);
            StateMachine.ChangeState(initState);
        }

        private void SetTurnStateForActiveUnit()
        {
            if (ActiveUnit == null)
                return;

            if (_playerTeam.Contains(ActiveUnit))
            {
                PlayerTurnState playerTurnState = new PlayerTurnState(StateMachine, ActiveUnit);
                StateMachine.ChangeState(playerTurnState);
            }
        }
    }
}
