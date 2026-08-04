using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    public class AttackCommand : ICombatCommand
    {
        private readonly int _attackerId;
        private readonly int _targetId;

        private readonly TurnBasedCombatEngine _engine;
        public int APCost { get; private set; }

        public AttackCommand(int attackerId, int targetId, int apCost, TurnBasedCombatEngine engine)
        {
            _attackerId = attackerId;
            _targetId = targetId;
            APCost = apCost;
            _engine = engine;
        }

        public bool CanExecute()
        {
            Hero attacker = _engine.GetUnitById(_attackerId);
            Hero target = _engine.GetUnitById(_targetId);

            return Validate(attacker, target);
        }

        private bool Validate(Hero attacker, Hero target)
        {
            return attacker != null
                && target != null
                && attacker.CurrentHealth > 0
                && target.CurrentHealth > 0
                && attacker.CurrentActionPoints >= APCost;
        }

        public bool Execute()
        {
            Hero attacker = _engine.GetUnitById(_attackerId);
            Hero target = _engine.GetUnitById(_targetId);

            if (!Validate(attacker, target))
                return false;

            if (!attacker.TrySpendAP(APCost))
                return false;

            int damage = DamageCalculator.CalculateDamage(attacker, target);

            target.TakeDamage(damage);

            return true;
        }
    }
}

