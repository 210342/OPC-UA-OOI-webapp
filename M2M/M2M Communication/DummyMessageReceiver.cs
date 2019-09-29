using System;
using System.Linq;
using System.Collections.Generic;

namespace M2M_Communication
{
    public class DummyMessageReceiver : IMessageReceiver, IDisposable
    {
        private Guid? _lastReceivedMessageID;
        private ICollection<ISubscription> _subscriptions;

        public IMessageBus MessageBus { get; set; }
        public IMessageParser MessageParser { get; set; }
        public IReadOnlyCollection<ISubscription> Subscriptions => _subscriptions as IReadOnlyCollection<ISubscription>;

        public DummyMessageReceiver(IMessageBus bus, IMessageParser parser) 
            : this (bus, parser, new List<ISubscription>()) { }

        public DummyMessageReceiver(IMessageBus bus, IMessageParser parser, ICollection<ISubscription> subscriptions)
        {
            MessageParser = parser;
            MessageBus = bus;
            _subscriptions = subscriptions;
        }

        public void Subscribe(Guid id)
        {
            MessageBus?.Subscribe(id, NewMessage);
        }

        public void Unsubscribe(Guid id)
        {
            MessageBus?.Unsubscribe(id, NewMessage);
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }

        private void NewMessage(IMessage message)
        {
            if (!message.Id.Equals(_lastReceivedMessageID))
            {
                _lastReceivedMessageID = message.Id;
                MessageParser.Parse(message);
            }
        }

        private void UnsubscribeAll()
        {
            if (_subscriptions is null)
            {
                return;
            }
            foreach (ISubscription subscription in _subscriptions)
            {
                MessageBus?.Unsubscribe(subscription.TypeId, subscription.NewMessage);
            }
        }
    }
}
