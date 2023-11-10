using System;
using System.Collections.Generic;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Treasure
{
    public class Inventory : IEventSender
    {
        private Dictionary<Id, IInventoryItem> inventory = new();

        public void ForeachItem(Action<IInventoryItem> dlg)
        {
            foreach (var countableResource in inventory)
            {
                dlg(countableResource.Value);
            }
        }

        public IInventoryItem GetOrCreate(Id id, Func<IInventoryItem> resourceFactory)
        {
            IInventoryItem item;
            if (inventory.TryGetValue(id, out item))
                return item;

            item = resourceFactory();
            inventory.Add(id, item);
            ServiceLocator.ServiceLocator.GetService<EventBus.EventBus>()
                .Raise(new InventoryItemAddedEvent(item, this));
            return item;
        }

        public IInventoryItem GetOrCreate(Id id) => GetOrCreate(id, () => new InventoryResource( new Resource.Resource(id) ));

        public void Add(Id key, int amount)
        {
            var current = GetOrCreate(key);
            inventory[key].Set(current.Current + amount);
        }

        public void Remove(Id key, int amount)
        {
            var current = GetOrCreate(key);
            inventory[key].Set(Mathf.Max(current.Current - amount, 0));
        }
    }
}