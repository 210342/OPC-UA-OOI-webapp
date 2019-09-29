using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public class Subscription : ISubscription
    {
        public Guid TypeId { get; }

        public NewMessageEventHandler NewMessage { get; set; }

        public Subscription(Guid typeGuid)
        {
            TypeId = typeGuid;
        }

        public Subscription(Guid typeGuid, NewMessageEventHandler newMessage)
        {
            TypeId = typeGuid;
            NewMessage = newMessage;
        }
    }
}
