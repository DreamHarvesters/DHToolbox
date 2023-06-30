using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;

namespace DHToolbox.Runtime.DHToolboxAssembly.Treasure
{
    public struct ResourceAddedToTreasureEvent : IEvent
    {
        public IEventSender Sender { get; }

        public readonly ICountableResource AddedResource;

        public ResourceAddedToTreasureEvent(ICountableResource resource, IEventSender sender)
        {
            AddedResource = resource;
            Sender = sender;
        }
    }
}