using System;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public delegate void NewMessageEventHandler(IMessage message);

    public interface IMessageBus
    {
        public void Subscribe(Guid id, NewMessageEventHandler newMessageEventHandler);
        public void Unsubscribe(Guid id, NewMessageEventHandler newMessageEventHandler);
        public void SendMessage(IMessage message);
        public IEnumerable<IMessage> ReadOldMessages();
        public IEnumerable<IMessage> ReadOldMessagesByType(Type type);
        public IEnumerable<IMessage> ReadOldMessagesByTypeGuid(Guid typeGuid);
        public IEnumerable<IMessage> ReadOldMessagesBySubscription(ISubscription subscription);
    }
}
