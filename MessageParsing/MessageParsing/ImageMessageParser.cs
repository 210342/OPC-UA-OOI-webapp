using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class ImageMessageParser : MessageParser
    {
        private readonly IStringLocalizer<ImageMessageParser> _localizer;

        public ImageTemplate ImageTemplate { get; private set; }

        public ImageMessageParser(IStringLocalizer<ImageMessageParser> localizer, ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory)
            : base(configuration, subscriptionFactory)
        {
            _localizer = localizer;
        }

        public override void Initialise()
        {
            ImageTemplate = new ImageTemplate(Guid.NewGuid(), @"Template.jpg", 1300, 480);

            foreach (ISubscription subscription in GetSubscriptions())
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

        public override async Task InitialiseAsync()
        {
            await Task.Run(() =>
            {
                Initialise();
            })
            .ConfigureAwait(true);
        }
    }
}
