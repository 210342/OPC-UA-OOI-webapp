using System;
using System.Linq;
using System.Collections.Generic;

namespace M2MCommunication
{
    public class DummyMessageReceiver : IMessageReceiver
    {
        private readonly IDictionary<ISubscription, NewMessageEventHandler> _subscriptions 
            = new Dictionary<ISubscription, NewMessageEventHandler>();

        public IMessageBus MessageBus { get; set; }
        public IReadOnlyCollection<ISubscription> Subscriptions => _subscriptions.Keys as IReadOnlyCollection<ISubscription>;

        public DummyMessageReceiver(IMessageBus bus) 
        {
            MessageBus = bus;
        }

        public DummyMessageReceiver(
            IMessageBus bus, 
            List<ISubscription> subscriptions, 
            NewMessageEventHandler newMessageEventHandler)
        {
            MessageBus = bus;
            subscriptions.ForEach(s => Subscribe(s, newMessageEventHandler));
        }

        public void Subscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler)
        {
            MessageBus?.Subscribe(subscription, newMessageEventHandler);
            _subscriptions.Add(subscription, newMessageEventHandler);
        }

        public void Unsubscribe(ISubscription subscription, NewMessageEventHandler newMessageEventHandler)
        {
            MessageBus?.Unsubscribe(subscription, newMessageEventHandler);
            _subscriptions.Remove(subscription);
        }

        public void Dispose()
        {
            UnsubscribeAll();
        }

        private void UnsubscribeAll()
        {
            if (MessageBus != null && _subscriptions != null)
            {
                foreach (KeyValuePair<ISubscription, NewMessageEventHandler> pair in _subscriptions)
                {
                    MessageBus.Unsubscribe(pair.Key, pair.Value);
                }
                _subscriptions.Clear();
            }
        }
    }
}
