using System;
using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using UniRx;

namespace DHToolbox.Runtime.DHToolboxAssembly.Resource
{
    public class Resource : ICountableResource
    {
        public static ICountableResource Create(Id id, int amount)
        {
            var res = new Resource(id);
            res.Set(amount);
            return res;
        }

        private IntReactiveProperty amount;

        public void Set(int newValue) => this.amount.Value = newValue;

        public int Current => amount.Value;
        public IObservable<int> ObserveAmount => amount;
        public Id Id { get; }

        public Resource(Id id)
        {
            amount = new IntReactiveProperty(0);

            Id = id;
        }

        protected bool Equals(Resource other)
        {
            return Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Resource)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}