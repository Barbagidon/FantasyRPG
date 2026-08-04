using System.Collections.Generic;
using FantasyRPG.Core.Combat;
using NUnit.Framework;

namespace FantasyRPG.Core.Tests
{
    [TestFixture]
    public class CombatStateMachineTests
    {
        private class FakeCombatState : ICombatState
        {
            private readonly List<string> _log;
            private readonly string _name;

            public FakeCombatState(List<string> log, string name)
            {
                _log = log;
                _name = name;
            }

            public void Enter()
            {
                _log.Add($"{_name}.Enter");
            }

            public void Exit()
            {
                _log.Add($"{_name}.Exit");
            }
        }

        [Test]
        public void ChangeState_FromExistingState_CallsExitBeforeEnter()
        {
            List<string> log = new();
            FakeCombatState stateA = new(log, "A");
            FakeCombatState stateB = new(log, "B");

            CombatStateMachine machine = new();

            machine.ChangeState(stateA);
            machine.ChangeState(stateB);

            CollectionAssert.AreEqual(new[] { "A.Enter", "A.Exit", "B.Enter" }, log);
        }
    }
}
