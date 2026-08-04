using System;
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
            StateMachine = new();
            _playerTeam = playerTeam;
            _enemyTeam = enemyTeam;
            CurrentTurnIndex = 0;
            _turnOrder = new List<Hero>(_playerTeam);
            _turnOrder.AddRange(_enemyTeam);
            HashSet<int> seenIds = new();

            foreach (Hero unit in _turnOrder)
            {
                if (!seenIds.Add(unit.Id))
                {
                    throw new ArgumentException($"Duplicate Hero.Id: {unit.Id}");
                }
            }
        }

        public void StartCombat()
        {
            CurrentTurnIndex = 0;
            InitState initState = new(_turnOrder);
            StateMachine.ChangeState(initState);
            SetTurnStateForActiveUnit();
        }

        private void SetTurnStateForActiveUnit()
        {
            if (ActiveUnit == null)
                return;

            if (_playerTeam.Contains(ActiveUnit))
                StateMachine.ChangeState(new PlayerTurnState(ActiveUnit));
            else
                StateMachine.ChangeState(new EnemyTurnState(ActiveUnit));
        }

        public bool CheckCombatEnd()
        {
            if (_enemyTeam.TrueForAll(e => e.CurrentHealth <= 0))
            {
                StateMachine.ChangeState(new VictoryState());
                return true;
            }

            if (_playerTeam.TrueForAll(p => p.CurrentHealth <= 0))
            {
                StateMachine.ChangeState(new DefeatState());
                return true;
            }

            return false;
        }

        public void AdvanceTurn()
        {
            if (CheckCombatEnd())
                return;

            do
            {
                CurrentTurnIndex = (CurrentTurnIndex + 1) % _turnOrder.Count;
            } while (ActiveUnit != null && ActiveUnit.CurrentHealth <= 0);

            SetTurnStateForActiveUnit();
        }

        public Hero GetUnitById(int id)
        {
            foreach (Hero unit in _turnOrder)
            {
                if (unit.Id == id)
                    return unit;
            }

            return null;
        }
    }
}

