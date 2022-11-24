using System;

namespace GameFoundations.Runtime.Game
{
    public interface IStateOwner<StateType> : IObservable<StateType>
    {
        public void SetState(StateType stateType);
    }
}