using System.Collections.Generic;
using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    /// <summary>
    /// Состояние инициализации боя. Рассчитывает очередность ходов (Initiative Order) на основе скорости персонажей.
    /// </summary>
    public class InitState : ICombatState
    {
        private readonly List<Hero> _units;

        public InitState(List<Hero> units)
        {
            _units = units;
        }

        public void Enter()
        {
            _units.Sort((a, b) => b.Stats.Initiative.CompareTo(a.Stats.Initiative));
        }

        public void Exit() { }
    }
}

