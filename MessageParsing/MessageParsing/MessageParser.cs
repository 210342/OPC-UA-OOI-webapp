using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MessageParsing
{
    public abstract class MessageParser : IMessageParser
    {
        protected internal IConfiguration Configuration { get; }
        protected internal ISubscriptionFactory SubscriptionFactory { get; }
        protected internal ICollection<IProperty> Properties { get; } = new List<IProperty>();
        public IEnumerable<DrawableProperty> DrawableProperties => Properties?.OfType<DrawableProperty>();
        public IEnumerable<PrintableProperty> PrintableProperties => Properties?.OfType<PrintableProperty>();

        public MessageParser(ConfigurationService configuration, SubscriptionFactoryService subscriptionFactory)
        {
            Configuration = configuration.Configuration;
            SubscriptionFactory = subscriptionFactory.SubscriptionFactory;
        }


        public abstract void Initialise(PropertyChangedEventHandler handler);

        public async Task InitialiseAsync(PropertyChangedEventHandler handler)
        {
            await Task.Run(() =>
            {
                Initialise(handler);
            })
            .ConfigureAwait(true);
        }

        protected internal IEnumerable<ISubscription> GetSubscriptions(PropertyChangedEventHandler handler)
        {
            return Configuration.GetDataTypeNames().Select(typeName => SubscriptionFactory.GetSubscription(typeName, handler));
        }
    }
}
