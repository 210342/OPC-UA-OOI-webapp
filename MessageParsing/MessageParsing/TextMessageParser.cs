using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing.Model;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;

namespace MessageParsing
{
    public class TextMessageParser : MessageParser
    {
        private readonly IStringLocalizer<TextMessageParser> _localizer;

        public TextMessageParser(IStringLocalizer<TextMessageParser> localizer, ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory)
            : base(configuration, subscriptionFactory)
        {
            _localizer = localizer;
        }

        public override void Initialise(Func<Task> handler)
        {
            Properties.Clear();
            foreach (ISubscription subscription in Subscribe(handler))
            {
                Properties.Add(new PrintableProperty(subscription, new PropertyTemplate(subscription.TypeName, null, Color.Black, null)));
            }
        }
    }
}
