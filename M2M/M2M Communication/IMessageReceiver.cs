using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public interface IMessageReceiver
    {
        public IMessageBus MessageBus { get; set; }
        public IReadOnlyCollection<ISubscription> Subscriptions { get; }
        public IMessageParser MessageParser { get; set; }

        public void Subscribe(Guid id);
        public void Unsubscribe(Guid id);
    }
}
