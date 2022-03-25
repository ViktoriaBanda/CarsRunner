using System;
using System.Collections.Generic;
using SimpleEventBus.Events;

namespace SimpleEventBus.Interfaces
{
    public interface IDebuggableEventBus : IEventBus
    {
        Dictionary<Type, List<ISubscriptionHolder>> Subscriptions { get; }
    }
}