namespace DHToolbox.Runtime.EventBus
{
    public interface IEvent
    {
        IEventSender Sender { get; }
    }
}