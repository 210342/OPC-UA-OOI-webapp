using TemplateRepositories.Model;
using M2MCommunication.Core.Interfaces;
using M2MCommunication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferenceWebApplication.ReactiveInterface
{
    public abstract class MessageParser : IMessageParser, IConsumerViewModel
    {
        protected internal IMessageBus MessageBus { get; private set; }
        protected internal Func<Task> OnSubscriptionUpdated { get; set; }

        public virtual IEnumerable<PrintableProperty> PrintableProperties { get; } = new List<PrintableProperty>();

        public MessageParser(MessageBusService messageBus)
        {
            MessageBus = messageBus?.MessageBus;
        }

        public virtual Task InitialiseAsync(Func<Task> handler)
        {
            OnSubscriptionUpdated = handler;
            return MessageBus.InitialiseAsync(this);
        }

        public virtual void RefreshConfiguration()
        {
            MessageBus.RefreshConfiguration();
        }

        public abstract void AddSubscription(ISubscription subscription);

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
                MessageBus = null;

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
