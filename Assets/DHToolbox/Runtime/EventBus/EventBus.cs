using System;
using UniRx;

namespace GameFoundations.Runtime.EventBus
{
    public class EventBus
    {
        internal EventBus()
        {
        }

        private Subject<IEvent> eventRaised = new();

        public IDisposable Subscribe<T>(IObserver<IEvent> observer) where T : IEvent =>
            eventRaised.Where(@event => @event.GetType() == typeof(T)).Subscribe(observer);

        public void Raise(IEvent @event) => eventRaised.OnNext(@event);
    }
}