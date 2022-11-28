using DHToolbox.Runtime.Game.Events;
using GameFoundations.Runtime.ServiceLocator;

namespace DHToolbox.Runtime.Game
{
    public class Game
    {
        public GameState CurrentState { get; private set; }

        public virtual void SetState(GameState stateType)
        {
            var oldState = CurrentState;
            ServiceLocator.EventBus.Raise(new BeforeGameStateChanged());
            CurrentState = stateType;
            ServiceLocator.EventBus.Raise(new AfterGameStateChanged()
                { OldState = oldState, NewState = CurrentState });
        }
    }
}