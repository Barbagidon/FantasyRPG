using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    public class AttackCommand : ICombatCommand
    {
        private readonly Hero _attacker;
        private readonly Hero _target;
        public int APCost { get; private set; }

        public AttackCommand(Hero attacker, Hero target, int apCost)
        {
            _attacker = attacker;
            _target = target;
            APCost = apCost;
        }

        public bool CanExecute()
        {
            return _attacker != null
                && _target != null
                && _attacker.CurrentHealth > 0
                && _target.CurrentHealth > 0
                && _attacker.CurrentActionPoints >= APCost;
        }

        public bool Execute()
        {
            if (!CanExecute())
                return false;

            if (!_attacker.TrySpendAP(APCost))
                return false;

            int damage = DamageCalculator.CalculateDamage(_attacker, _target);

            _target.TakeDamage(damage);

            return true;
        }
    }
}
