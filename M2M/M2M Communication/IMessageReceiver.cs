using System;
using System.Collections.Generic;

namespace M2M_Communication
{
    public interface IMessageReceiver : IDisposable
    {
        public IMessageBus MessageBus { get; set; }
        public IReadOnlyCollection<ISubscription> Subscriptions { get; }
        public IMessageParser MessageParser { get; set; }

        public void Subscribe(ISubscription subscription);
        public void Unsubscribe(ISubscription subscription);
    }
}
