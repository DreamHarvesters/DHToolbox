using System;
using GameFoundations.Runtime.Singleton;
using UniRx;

namespace GameFoundations.Runtime.Game
{
    public class Game : Singleton<Game>, IStateOwner<GameState>
    {
        private ReactiveProperty<GameState> gameState = new(GameState.Init);

        public IDisposable Subscribe(IObserver<GameState> observer)
            => gameState.Subscribe(observer);

        public GameState CurrentState => gameState.Value;

        public virtual void SetState(GameState stateType) => gameState.Value = stateType;
    }
}