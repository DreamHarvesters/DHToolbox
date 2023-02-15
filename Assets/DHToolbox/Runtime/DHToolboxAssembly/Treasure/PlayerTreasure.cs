using System;
using System.Collections.Generic;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Identification;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;
using UnityEngine;

namespace DHToolbox.Runtime.DHToolboxAssembly.Treasure
{
    public class PlayerTreasure : IEventSender
    {
        private Dictionary<Id, ICountableResource> treasure;

        public void ForeachItem(Action<ICountableResource> dlg)
        {
            foreach (var countableResource in treasure)
            {
                dlg(countableResource.Value);
            }
        }

        public ICountableResource GetOrAdd(Id id, Func<ICountableResource> resourceFactory)
        {
            ICountableResource resource;
            if (treasure.TryGetValue(id, out resource))
                return resource;

            resource = resourceFactory();
            treasure.Add(id, resource);
            ServiceLocator.ServiceLocator.GetService<EventBus.EventBus>()
                .Raise(new ResourceAddedToTreasureEvent(resource, this));
            return resource;
        }

        public ICountableResource GetOrAdd(Id id) => GetOrAdd(id, () => new Resource.Resource(id));

        public void Add(Id key, int amount)
        {
            var current = GetOrAdd(key);
            treasure[key].Set(current.Current + amount);
        }

        public void Remove(Id key, int amount)
        {
            var current = GetOrAdd(key);
            treasure[key].Set(Mathf.Max(current.Current - amount, 0));
        }

        public PlayerTreasure()
        {
            treasure = new Dictionary<Id, ICountableResource>();
        }
    }
}