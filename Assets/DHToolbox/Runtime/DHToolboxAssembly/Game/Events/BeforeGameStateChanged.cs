using DHToolbox.Runtime.DHToolboxAssembly.EventBus;

namespace DHToolbox.Runtime.DHToolboxAssembly.Game.Events
{
    public struct BeforeGameStateChanged : IEvent
    {
        public IEventSender Sender { get; set; }
    }
}