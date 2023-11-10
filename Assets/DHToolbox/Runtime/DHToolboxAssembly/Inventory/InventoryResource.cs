using System;
using DHToolbox.Runtime.DHToolboxAssembly.Identification;

namespace DHToolbox.Runtime.DHToolboxAssembly.Treasure
{
    public class InventoryResource : IInventoryItem
    {
        public InventoryResource(Resource.Resource resource)
        {
            this.resource = resource;
        }

        private Resource.Resource resource;

        public Id Id => resource.Id;
        public void Set(int newValue) => resource.Set(newValue);

        public int Current => resource.Current;
        public IObservable<int> ObserveAmount => resource.ObserveAmount;
    }
}