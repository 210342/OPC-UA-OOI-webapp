using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{

    public class MessageBus : IMessageBus
    {
        private readonly Queue<IMessage> _messages = new Queue<IMessage>();

        public event NewMessageEventHandler NewMessage;

        public IEnumerable<IMessage> ReadOldMessages()
        {
            return _messages;
        }

        public IEnumerable<IMessage> ReadOldMessagesBySubscription(ISubscription subscription)
        {
            return _messages.Where(message =>
                subscription.Types.Any(tuple =>
                    tuple.Item1.Equals(message.TypeGuid) 
                    || tuple.Item2.Equals(message.GetType())));
        }

        public IEnumerable<IMessage> ReadOldMessagesByType(Type type)
        {
            return _messages.Where(message => message.GetType().IsAssignableFrom(type));
        }

        public IEnumerable<IMessage> ReadOldMessagesByTypeGuid(Guid typeGuid)
        {
            return _messages.Where(message => message.TypeGuid.Equals(typeGuid));
        }

        public void SendMessage(IMessage message)
        {
            _messages.Enqueue(message);
            NewMessage?.Invoke(message);
        }
    }
}
