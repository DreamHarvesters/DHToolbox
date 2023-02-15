using System.Collections.Generic;
using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Game.Initialization;

namespace DHToolbox.Runtime.DHToolboxAssembly.Game.Events
{
    public struct BeforeInitializeEvent : IEvent
    {
        public IEventSender Sender { get; }

        public readonly List<IInitializable> Initializables;

        public BeforeInitializeEvent(IEventSender sender) : this()
        {
            Sender = sender;

            Initializables = new List<IInitializable>();
        }
    }
}