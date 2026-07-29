using FantasyRPG.Core.Stats;

namespace FantasyRPG.Core.Combat
{
    public class AttackCommand : ICombatCommand
    {
        private readonly Hero _attacker;
        private readonly Hero _target;
        public int APCost { get; private set; }
        private readonly bool _isCritical;

        public AttackCommand(Hero attacker, Hero target, int apCost, bool isCritical = false)
        {
            _attacker = attacker;
            _target = target;
            APCost = apCost;
            _isCritical = isCritical;
        }

        public bool CanExecute()
        {
            return _attacker != null
                && _target != null
                && _attacker.Stats.CurrentHealth > 0
                && _target.Stats.CurrentHealth > 0;
        }

        public bool Execute()
        {
            if (!CanExecute())
                return false;

            int damage = DamageCalculator.CalculateDamage(_attacker, _target, _isCritical);

            _target.TakeDamage(damage);

            return true;
        }
    }
}
