using InterfaceModel.Model;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class TextMessageParser : MessageParser
    {
        public TextMessageParser(ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory)
            : base(configuration, subscriptionFactory) { }

        public override void Initialise(Func<Task> handler)
        {
            Properties.Clear();
            foreach (ISubscription subscription in Subscribe(handler))
            {
                Properties.Add(new PrintableProperty(subscription, new PropertyTemplate(subscription.UaTypeMetadata.TypeName, null, Color.Black, null)));
            }
        }
    }
}
