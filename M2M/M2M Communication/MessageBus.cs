using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{

    public class MessageBus : IMessageBus
    {
        private readonly Queue<IMessage> _messages = new Queue<IMessage>();

        private readonly Dictionary<Guid, ICollection<NewMessageEventHandler>> _subscriptions
            = new Dictionary<Guid, ICollection<NewMessageEventHandler>>();

        public IEnumerable<IMessage> ReadOldMessages()
        {
            return _messages;
        }

        public IEnumerable<IMessage> ReadOldMessagesBySubscription(ISubscription subscription)
        {
            return _messages.Where(message =>
                message.TypeGuid.Equals(subscription.TypeId));
        }

        public IEnumerable<IMessage> ReadOldMessagesByType(Type type)
        {
            return _messages.Where(message => message.GetType().IsAssignableFrom(type));
        }

        public IEnumerable<IMessage> ReadOldMessagesByTypeGuid(Guid typeGuid)
        {
            return _messages.Where(message => message.TypeGuid.Equals(typeGuid));
        }

        public void Subscribe(Guid id, NewMessageEventHandler newMessageEventHandler)
        {
            if (_subscriptions.TryGetValue(id, out ICollection<NewMessageEventHandler> events))
            {
                events.Add(newMessageEventHandler);
            }
            else
            {
                _subscriptions.Add(id, new List<NewMessageEventHandler>(new[] { newMessageEventHandler }));
            }
        }

        public void Unsubscribe(Guid id, NewMessageEventHandler newMessageEventHandler)
        {
            if (_subscriptions.TryGetValue(id, out ICollection<NewMessageEventHandler> events))
            {
                events.Remove(newMessageEventHandler);
                if (events.Count == 0)
                {
                    _subscriptions.Remove(id);
                }
            }
        }

        public void SendMessage(IMessage message)
        {
            _messages.Enqueue(message);
            if (_subscriptions.TryGetValue(message.TypeGuid, out ICollection<NewMessageEventHandler> events))
            {
                foreach (var _event in events)
                {
                    _event.Invoke(message);
                }
            }
        }
    }
}
