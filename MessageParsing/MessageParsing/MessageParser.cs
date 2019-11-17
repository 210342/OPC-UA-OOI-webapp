using InterfaceModel.Model;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageParsing
{
    public abstract class MessageParser : IMessageParser
    {
        protected internal IConfiguration Configuration { get; }
        protected internal ISubscriptionFactory SubscriptionFactory { get; }
        public virtual IEnumerable<PrintableProperty> PrintableProperties { get; } = new List<PrintableProperty>();

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
            _subscriptions = Configuration
                .GetTypeMetadata()
                .Select(uaTypeMetadata => SubscriptionFactory.Subscribe(uaTypeMetadata, (sender, args) => Task.Run(() => handler?.Invoke())));
            return _subscriptions;
        }

        #region IDisposable Support
        /// <summary>
        /// For IDisposable implementation
        /// </summary>
        private IEnumerable<ISubscription> _subscriptions = Enumerable.Empty<ISubscription>();
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _subscriptions?.ToList()?.ForEach(subscription => subscription.Disable());
                }

                _subscriptions = Enumerable.Empty<ISubscription>();

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
