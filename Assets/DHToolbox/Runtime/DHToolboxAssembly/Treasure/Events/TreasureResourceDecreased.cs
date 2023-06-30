using DHToolbox.Runtime.DHToolboxAssembly.EventBus;
using DHToolbox.Runtime.DHToolboxAssembly.Resource;

namespace DHToolbox.Runtime.DHToolboxAssembly.Treasure
{
    public struct TreasureResourceDecreased : IEvent
    {
        public IEventSender Sender { get; }

        public readonly ICountableResource Resource;

        public TreasureResourceDecreased(ICountableResource resource, IEventSender sender)
        {
            Resource = resource;
            Sender = sender;
        }
    }
}