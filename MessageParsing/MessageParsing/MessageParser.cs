using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MessageParsing
{
    public abstract class MessageParser : IMessageParser
    {
        /// <summary>
        /// For IDisposable implementation
        /// </summary>
        private IEnumerable<ISubscription> _subscriptions;

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


        public abstract void Initialise(Func<Task> handler);

        public async Task InitialiseAsync(Func<Task> handler)
        {
            await Task.Run(() =>
            {
                Initialise(handler);
            })
            .ConfigureAwait(true);
        }

        protected internal IEnumerable<ISubscription> Subscribe(Func<Task> handler)
        {
            PropertyChangedEventHandler propertyChangedEventHandler = (sender, args) => Task.Run(() => handler?.Invoke());
            _subscriptions = Configuration
                .GetDataTypeNames()
                .Select(typeName => SubscriptionFactory.Subscribe(typeName, propertyChangedEventHandler));
            return _subscriptions;
        }

        public void Dispose()
        {
            _subscriptions.ToList().ForEach(subscription => subscription.Disable());
        }
    }
}
