using System;

namespace DHToolbox.Runtime.DHToolboxAssembly.AmountOwner
{
    public interface IReactiveNumber<T>
    {
        void Set(T newValue);

        T Current { get; }

        IObservable<T> ObserveAmount { get; }
    }
}