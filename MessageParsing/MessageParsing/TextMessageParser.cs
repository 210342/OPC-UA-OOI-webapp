using InterfaceModel.Model;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class TextMessageParser : MessageParser
    {
        private new ICollection<PrintableProperty> PrintableProperties => base.PrintableProperties as ICollection<PrintableProperty>;

        public TextMessageParser(ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory)
            : base(configuration, subscriptionFactory) { }

        public override void Initialise(Func<Task> handler)
        {
            PrintableProperties.Clear();
            foreach (ISubscription subscription in Subscribe(handler))
            {
                PrintableProperties.Add(new PrintableProperty(subscription, new PropertyTemplate(subscription.UaTypeMetadata.TypeName, null, Color.Black, null)));
            }
        }
    }
}
