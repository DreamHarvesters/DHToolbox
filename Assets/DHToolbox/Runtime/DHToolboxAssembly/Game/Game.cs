using System;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Game.Events;
using UniRx;

namespace DHToolbox.Runtime.DHToolboxAssembly.Game
{
    public class Game : IEventSender
    {
        private ReactiveProperty<GameState> state = new ReactiveProperty<GameState>();

        public IObservable<GameState> ObserveState => state;

        public GameState CurrentState
        {
            get => state.Value;
            private set => state.Value = value;
        }

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