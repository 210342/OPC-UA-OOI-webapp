using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public interface IMessageReceiver
    {
        public IMessageBus MessageBus { get; set; }
        public ICollection<ISubscription> Subscriptions { get; set; }
        public IMessageParser MessageParser { get; set; }
    }
}
