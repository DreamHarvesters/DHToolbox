using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;

namespace DHToolbox.Runtime.DHToolboxAssembly.Treasure
{
    public struct InventoryItemAddedEvent : IEvent
    {
        public IEventSender Sender { get; }

        public readonly ICountableResource AddedResource;

        public InventoryItemAddedEvent(ICountableResource resource, IEventSender sender)
        {
            AddedResource = resource;
            Sender = sender;
        }
    }
}