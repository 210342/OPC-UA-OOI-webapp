using System;
using System.Collections.Generic;

namespace M2MCommunication
{
    public interface IMessageReceiver : IDisposable
    {
        IMessageBus MessageBus { get; set; }
        IReadOnlyCollection<ISubscription> Subscriptions { get; }

        void Subscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler);
        void Unsubscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler);
    }
}
