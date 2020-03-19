using ReactiveHMI.TemplateRepositories.Model;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using ReactiveHMI.M2MCommunication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactiveHMI.ReferenceWebApplication.ReactiveInterface
{
    public abstract class MessageParser : IMessageParser, IConsumerViewModel
    {
        protected internal IMessageBus MessageBus { get; private set; }
        public event Func<Task> OnSubscriptionUpdated;

        public virtual IEnumerable<PrintableProperty> PrintableProperties { get; } = new List<PrintableProperty>();

        public MessageParser(MessageBusService messageBus)
        {
            MessageBus = messageBus?.MessageBus;
        }

        public virtual Task InitialiseAsync()
        {
            return MessageBus.InitialiseAsync(this);
        }

        public virtual void RefreshConfiguration()
        {
            MessageBus.RefreshConfiguration();
        }

        public abstract void AddSubscription(ISubscription subscription);

        protected internal Task InvokeSubscriptionUpdated()
        {
            return OnSubscriptionUpdated.Invoke();
        }
    }
}
