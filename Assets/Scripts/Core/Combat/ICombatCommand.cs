namespace FantasyRPG.Core.Combat
{
    public interface ICombatCommand
    {
        bool CanExecute();
        bool Execute();
    }
}
