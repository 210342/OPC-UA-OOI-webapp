using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication
{
    public delegate void NewMessageEventHandler(IMessage message);

    public interface IMessageBus
    {
        void Subscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler);
        bool Unsubscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler);
        void SendMessage(IMessage message);
        IEnumerable<IMessage> ReadOldMessages();
        IEnumerable<IMessage> ReadOldMessagesByType(Type type);
        IEnumerable<IMessage> ReadOldMessagesByTypeGuid(Guid typeGuid);
        IEnumerable<IMessage> ReadOldMessagesBySubscription(ISubscription subscription);
    }
}
