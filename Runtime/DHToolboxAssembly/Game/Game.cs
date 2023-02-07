using Cysharp.Threading.Tasks;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Game.Events;

namespace DHToolbox.Runtime.DHToolboxAssembly.Game
{
    public class Game : IEventSender
    {
        public GameState CurrentState { get; private set; }

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
                .ContinueWith(() => EventBus.Raise(new AfterInitializeEvent(this)));
        }

        public void MainMenu() => SetState(GameState.MainMenu);

        public void LoadLevel() => SetState(GameState.LoadingLevel);

        public void StartGame() => SetState(GameState.InGame);

        public void WinGame() => SetState(GameState.Success);
        public void LoseGame() => SetState(GameState.Fail);
    }
}