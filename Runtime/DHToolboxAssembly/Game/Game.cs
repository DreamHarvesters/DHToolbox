using DHToolbox.Runtime.EventBus;
using DHToolbox.Runtime.Game.Events;

namespace DHToolbox.Runtime.Game
{
    public class Game : IEventSender
    {
        public GameState CurrentState { get; private set; }

        protected virtual void SetState(GameState stateType)
        {
            var eventBus = ServiceLocator.ServiceLocator.GetService<EventBus.EventBus>();

            var oldState = CurrentState;
            eventBus.Raise(new BeforeGameStateChanged() { Sender = this });
            CurrentState = stateType;
            eventBus.Raise(new AfterGameStateChanged()
                { OldState = oldState, NewState = CurrentState, Sender = this });
        }

        public void Initialize() => SetState(GameState.Initializing);

        public void MainMenu() => SetState(GameState.MainMenu);

        public void LoadLevel() => SetState(GameState.LoadingLevel);

        public void StartGame() => SetState(GameState.InGame);

        public void WinGame() => SetState(GameState.Success);
        public void LoseGame() => SetState(GameState.Fail);
    }
}