namespace DHToolbox.Runtime.DHToolboxAssembly.Commands
{
    public interface ICommand
    {
        void Initializa();
        void Execute();
    }
}