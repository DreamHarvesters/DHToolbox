using System;

namespace Foundations.Scripts.AmountOwner
{
    public interface IReactiveNumber<T>
    {
        void Set(T newValue);

        T Current { get; }

        IObservable<T> ObserveAmount { get; }
    }
}