using System.Collections.Generic;
using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    public class InitState : ICombatState
    {
        private readonly CombatStateMachine _stateMachine;
        private readonly List<Hero> _units;

        public InitState(CombatStateMachine stateMachine, List<Hero> units)
        {
            _stateMachine = stateMachine;
            _units = units;
        }

        public void Enter()
        {
            _units.Sort((a, b) => b.Stats.Speed.CompareTo(a.Stats.Speed));
        }

        public void Exit() { }
    }
}
