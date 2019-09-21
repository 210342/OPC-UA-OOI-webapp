using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace M2M_Communication
{
    public class DummyMessageReceiver : IMessageReceiver
    {
        private Guid? _lastReceivedMessageID;

        public IMessageBus MessageBus { get; set; }
        public ICollection<ISubscription> Subscriptions { get; set; }
        public IMessageParser MessageParser { get; set; }

        public DummyMessageReceiver()
        {
            MessageBus.NewMessage += NewMessage;
        }

        public DummyMessageReceiver(IEnumerable<ISubscription> subscriptions) : this()
        {
            Subscriptions = new HashSet<ISubscription>(subscriptions);
        }

        private void NewMessage(IMessage message)
        {
            if (Subscriptions.Any(subscription => 
                    subscription.Types.Any(tuple => 
                        tuple.Item1.Equals(message.TypeGuid) || 
                        tuple.Item2.IsAssignableFrom(message.GetType())
                    )
                ) && !message.ID.Equals(_lastReceivedMessageID))
            {
                _lastReceivedMessageID = message.ID;
                MessageParser.Parse(message);
            }
        }
    }
}
