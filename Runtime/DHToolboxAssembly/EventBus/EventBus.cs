using System;
using UniRx;

namespace DHToolbox.Runtime.DHToolboxAssembly.EventBus
{
    public class EventBus
    {
        internal EventBus()
        {
        }

        private Subject<IEvent> eventRaised = new();

        public IObservable<T> AsObservable<T>() where T : IEvent =>
            eventRaised.Where(@event => @event.GetType() == typeof(T)).Select(@event => (T)@event);

        public IObservable<IEvent> AsObservable(Type eventType) =>
            eventRaised.Where(@event => @event.GetType() == eventType);


        public void Raise(IEvent @event) => eventRaised.OnNext(@event);
    }
}