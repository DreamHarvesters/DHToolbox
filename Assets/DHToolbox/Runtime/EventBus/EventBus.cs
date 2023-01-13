using System;
using UniRx;

namespace DHToolbox.Runtime.EventBus
{
    public class EventBus : IObservable<IEvent>
    {
        internal EventBus()
        {
        }

        private Subject<IEvent> eventRaised = new();

        public IDisposable Subscribe<T>(IObserver<IEvent> observer) where T : IEvent =>
            eventRaised.Where(@event => @event.GetType() == typeof(T)).Subscribe(observer);

        public void Raise(IEvent @event) => eventRaised.OnNext(@event);

        public IDisposable Subscribe(IObserver<IEvent> observer) =>
            eventRaised.Subscribe(observer);
    }
}