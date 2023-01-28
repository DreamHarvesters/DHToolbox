using DHToolbox.Runtime.EventBus;
using Foundations.Scripts.Identification;
using Foundations.Scripts.Resource;

namespace GameAssets.Scripts
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