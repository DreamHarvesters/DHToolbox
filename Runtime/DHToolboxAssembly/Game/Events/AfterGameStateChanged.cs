using DHToolbox.Runtime.DHToolboxAssembly.EventBus;

namespace DHToolbox.Runtime.DHToolboxAssembly.Game.Events
{
    public struct AfterGameStateChanged : IEvent
    {
        public IEventSender Sender { get; set; }
        public GameState OldState;
        public GameState NewState;
    }
}