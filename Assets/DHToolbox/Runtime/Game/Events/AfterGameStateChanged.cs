using DHToolbox.Runtime.EventBus;

namespace DHToolbox.Runtime.Game.Events
{
    public struct AfterGameStateChanged : IEvent
    {
        public IEventSender Sender { get; set; }
        public GameState OldState;
        public GameState NewState;
    }
}