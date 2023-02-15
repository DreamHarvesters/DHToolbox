using System;
using UniRx;

namespace DHToolbox.Runtime.DHToolboxAssembly.EventBus
{
    public class EventBuffer<T> : IDisposable where T : IEvent
    {
        private ReplaySubject<T> subject;
        private IDisposable subscription;

        public IObservable<T> Observe => subject;

        public EventBuffer()
        {
            subject = new ReplaySubject<T>();
            subscription = ServiceLocator.ServiceLocator.GetService<EventBus>().AsObservable<T>().Subscribe(subject);
        }

        public EventBuffer(int bufferSize)
        {
            subject = new ReplaySubject<T>(bufferSize);
            subscription = ServiceLocator.ServiceLocator.GetService<EventBus>().AsObservable<T>().Subscribe(subject);
        }

        public void Dispose()
        {
            subscription.Dispose();
        }
    }
}