using DHToolbox.Runtime.DHToolboxAssembly.EventBus;

namespace DHToolbox.Runtime.DHToolboxAssembly.Game.Events
{
    public struct AfterInitializeEvent : IEvent
    {
        public IEventSender Sender { get; }

        public AfterInitializeEvent(IEventSender sender)
        {
            Sender = sender;
        }
    }
}