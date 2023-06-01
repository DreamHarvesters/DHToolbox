namespace DHToolbox.Runtime.DHToolboxAssembly.Commands.Commands
{
    public class UnityEvent : ICommand
    {
        public UnityEngine.Events.UnityEvent Event;

        public void Initializa()
        {
        }

        public void Execute()
        {
            Event?.Invoke();
        }
    }
}