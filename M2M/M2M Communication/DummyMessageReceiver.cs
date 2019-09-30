using System;
using System.Linq;
using System.Collections.Generic;

namespace M2M_Communication
{
    public class DummyMessageReceiver : IMessageReceiver
    {
        private Guid? _lastReceivedMessageID;
        private List<ISubscription> _subscriptions = new List<ISubscription>();

        public IMessageBus MessageBus { get; set; }
        public IMessageParser MessageParser { get; set; }
        public IReadOnlyCollection<ISubscription> Subscriptions => _subscriptions as IReadOnlyCollection<ISubscription>;

        public DummyMessageReceiver(IMessageBus bus, IMessageParser parser) 
            : this (bus, parser, new List<ISubscription>()) { }

        public DummyMessageReceiver(IMessageBus bus, IMessageParser parser, List<ISubscription> subscriptions)
        {
            MessageParser = parser;
            MessageBus = bus;
            subscriptions.ForEach(s => Subscribe(s));
        }

        public void Subscribe(ISubscription subscription)
        {
            MessageBus?.Subscribe(subscription, NewMessage);
            _subscriptions.Add(subscription);
        }

        public void Unsubscribe(ISubscription subscription)
        {
            MessageBus?.Unsubscribe(subscription, NewMessage);
            _subscriptions.Remove(subscription);
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
                MessageParser?.Parse(message);
            }
        }

        private void UnsubscribeAll()
        {
            if (MessageBus != null)
            {
                _subscriptions?.RemoveAll(sub => MessageBus.Unsubscribe(sub, NewMessage));
            }
        }
    }
}
