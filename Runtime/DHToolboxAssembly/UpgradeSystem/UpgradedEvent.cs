using DHToolbox.Runtime.DHToolboxAssembly.EventBus;

namespace DHToolbox.Runtime.DHToolboxAssembly.UpgradeSystem
{
    public struct UpgradedEvent : IEvent
    {
        public IEventSender Sender { get; }

        public readonly IUpgradableAttributes UpgradedAttributes;

        public UpgradedEvent(IUpgradableAttributes upgradedAttributes, IEventSender sender)
        {
            UpgradedAttributes = upgradedAttributes;
            Sender = sender;
        }
    }
}