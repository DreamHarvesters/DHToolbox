using DHToolbox.Runtime.EventBus;

namespace DHToolbox.Runtime.Game.Events
{
    public struct AfterGameStateChanged : IEvent
    {
        public GameState OldState;
        public GameState NewState;
    }
}