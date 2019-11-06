using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing.Model;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
        public ImageTemplate ImageTemplate { get; private set; }

        public ImageMessageParser(ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory)
            : base(configuration, subscriptionFactory) { }

        public override void Initialise(Func<Task> handler)
        {
            ImageTemplate = new ImageTemplate(Guid.NewGuid(), @"Template.jpg", 1300, 480);

            foreach (ISubscription subscription in Subscribe(handler))
            {
                if (ImageTemplate.Properties.Where(p => p.Template.Name.Equals(subscription.TypeName)).FirstOrDefault() is IProperty property)
                {
                    property.Subscription = subscription;
                    Properties.Add(property);
                }
                else
                {
                    Properties.Add(new PrintableProperty(subscription, new PropertyTemplate(subscription.TypeName, null, Color.Black, null)));
                }
            }
        }
    }
}
