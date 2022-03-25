using SimpleEventBus.Events;

namespace SimpleEventBus.Interfaces
{
    public interface ISubscriptionHolder
    {
        void Invoke(EventBase eventBase);
    }
}