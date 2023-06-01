using Cysharp.Threading.Tasks;
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

        public IObservable<Unit> ObserveGameEnd =>
            state.Where(gameState => gameState is GameState.Fail or GameState.Success).AsUnitObservable();

        public IObservable<Unit> ObserveGameStart =>
            state.Where(gameState => gameState is GameState.InGame).AsUnitObservable();

        public GameState CurrentState
        {
            get => state.Value;
            private set => state.Value = value;
        }

        private EventBus.EventBus EventBus => ServiceLocator.ServiceLocator.GetService<EventBus.EventBus>();

        protected virtual void SetState(GameState stateType)
        {
            var oldState = CurrentState;
            EventBus.Raise(new BeforeGameStateChanged() { Sender = this });
            CurrentState = stateType;
            EventBus.Raise(new AfterGameStateChanged()
                { OldState = oldState, NewState = CurrentState, Sender = this });
        }

        public void Initialize()
        {
            SetState(GameState.Initializing);
            var initEvent = new BeforeInitializeEvent(this);
            EventBus.Raise(initEvent);
            UniTask.WhenAll(initEvent.Initializables.Select(initializable => initializable.Initialize()))
                .ContinueWith(() => EventBus.Raise(new AfterInitializeEvent(this)))
                .ContinueWith(LoadLevel)
                .ContinueWith(() =>
                    EventBus.AsObservable<AfterGameStateChanged>()
                        .First(changed => changed.NewState == GameState.LevelLoadingComplete).ToUniTask(true))
                .ContinueWith(_ => MainMenu());
        }

        public void MainMenu() => SetState(GameState.MainMenu);

        public void LoadLevel() => SetState(GameState.LoadingLevel);

        public void CompleteLevelLoading() => SetState(GameState.LevelLoadingComplete);

        public void StartGame() => SetState(GameState.InGame);

        public void WinGame() => SetState(GameState.Success);
        public void LoseGame() => SetState(GameState.Fail);
    }
}