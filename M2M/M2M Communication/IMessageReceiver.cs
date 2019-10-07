using System;
using System.Collections.Generic;

namespace M2MCommunication
{
    public interface IMessageReceiver : IDisposable
    {
        public IMessageBus MessageBus { get; set; }
        public IReadOnlyCollection<ISubscription> Subscriptions { get; }

        public void Subscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler);
        public void Unsubscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler);
    }
}
