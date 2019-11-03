using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing.Model;
using System.Collections.Generic;
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


        public abstract void Initialise();
        public abstract Task InitialiseAsync();
        protected internal IEnumerable<ISubscription> GetSubscriptions()
        {
            return Configuration.GetDataTypeNames().Select(typeName => SubscriptionFactory.GetSubscription(typeName, (_, __) => { }));
        }
    }
}
