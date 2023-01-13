namespace DHToolbox.Runtime.Commands
{
    public interface ICommand
    {
        void Initializa();
        void Execute();
    }
}