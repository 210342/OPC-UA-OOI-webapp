using System;
using System.Linq;
using System.Collections.Generic;

namespace M2M_Communication
{
    public class DummyMessageReceiver : IMessageReceiver, IDisposable
    {
        private Guid? _lastReceivedMessageID;

        public IMessageBus MessageBus { get; set; }
        public ICollection<ISubscription> Subscriptions { get; set; } = new List<ISubscription>();
        public IMessageParser MessageParser { get; set; }

        public DummyMessageReceiver(IMessageBus bus, IMessageParser parser)
        {
            MessageParser = parser;
            MessageBus = bus;
            MessageBus.NewMessage += NewMessage;
        }

        private void NewMessage(IMessage message)
        {
            if (Subscriptions.Any(subscription => 
                    subscription.Types.Any(tuple => 
                        tuple.Item1.Equals(message.TypeGuid) || 
                        tuple.Item2.IsAssignableFrom(message.GetType())
                    )
                ) && !message.Id.Equals(_lastReceivedMessageID))
            {
                _lastReceivedMessageID = message.Id;
                MessageParser.Parse(message);
            }
        }

        public void Dispose()
        {
            if (MessageBus != null)
            {
                MessageBus.NewMessage -= NewMessage; 
            }
        }
    }
}
