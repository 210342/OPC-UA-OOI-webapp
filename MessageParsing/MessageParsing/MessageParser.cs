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
        private readonly UaLibrarySettings uaLibrarySettings;
        protected internal IMessageBus MessageBus { get; private set; }

        public virtual IEnumerable<PrintableProperty> PrintableProperties => new List<PrintableProperty>();

        public MessageParser(MessageBusService messageBus, UaLibrarySettings settings)
        {
            MessageBus = messageBus.MessageBus;
            uaLibrarySettings = settings;
        }

        public virtual Task InitialiseAsync(Func<Task> handler)
        {
            return MessageBus.InitialiseAsync(uaLibrarySettings, (obj, sub) =>
                {
                    sub.Enable((obj, args) => handler());
                    OnSubscriptionReceived(sub);
                }
            );
        }
        public virtual void RefreshConfiguration()
        {
            MessageBus.RefreshConfiguration();
        }

        protected internal abstract void OnSubscriptionReceived(ISubscription subscription);

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
                    MessageBus.Dispose();
                }

                _subscriptions = Enumerable.Empty<ISubscription>();
                MessageBus = null;

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
