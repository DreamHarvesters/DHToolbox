using DHToolbox.Runtime.EventBus;

namespace DHToolbox.Runtime.Game.Events
{
    public struct BeforeGameStateChanged : IEvent
    {
        public IEventSender Sender { get; set; }
    }
}