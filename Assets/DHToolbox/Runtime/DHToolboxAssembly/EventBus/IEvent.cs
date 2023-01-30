namespace DHToolbox.Runtime.DHToolboxAssembly.EventBus
{
    public interface IEvent
    {
        IEventSender Sender { get; }
    }
}