using System;
using UniRx;

namespace DHToolbox.Runtime.DHToolboxAssembly.EventBus
{
    public class EventBus : IObservable<IEvent>
    {
        internal EventBus()
        {
        }

        private Subject<IEvent> eventRaised = new();
        private ReplaySubject<IEvent> bufferedEventRaised = new();

        public IDisposable Subscribe<T>(IObserver<IEvent> observer) where T : IEvent =>
            eventRaised.Where(@event => @event.GetType() == typeof(T)).Subscribe(observer);

        public IObservable<T> AsObservable<T>() where T : IEvent =>
            eventRaised.Where(@event => @event.GetType() == typeof(T)).Select(@event => (T)@event);

        public IObservable<T> AsObservableBuffered<T>() where T : IEvent =>
            bufferedEventRaised.Where(@event => @event.GetType() == typeof(T)).Select(@event => (T)@event);

        public void Raise(IEvent @event) => eventRaised.OnNext(@event);

        public void RaiseBuffered(IEvent @event) => bufferedEventRaised.OnNext(@event);

        public IDisposable Subscribe(IObserver<IEvent> observer) =>
            eventRaised.Subscribe(observer);
    }
}