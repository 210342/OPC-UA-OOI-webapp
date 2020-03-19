using ReactiveHMI.M2MCommunication.Core.Exceptions;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using UAOOI.Configuration.Networking;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData;

namespace ReactiveHMI.M2MCommunication.UaooiInjections.Components
{
    [Export(typeof(IMessageBus))]
    public class UaooiMessageBus : DataManagementSetup, IMessageBus
    {
        private readonly ILogger _logger;

        [ImportingConstructor]
        public UaooiMessageBus(
            IConfigurationFactory configurationFactory,
            IEncodingFactory encodingFactory,
            IBindingFactory subscriptionFactory,
            IMessageHandlerFactory messageHandlerFactory,
            ILogger logger)
        {
            ConfigurationFactory = configurationFactory
                ?? throw new ComponentNotInitialisedException(nameof(configurationFactory));
            EncodingFactory = encodingFactory
                ?? throw new ComponentNotInitialisedException(nameof(encodingFactory));
            BindingFactory = subscriptionFactory
                ?? throw new ComponentNotInitialisedException(nameof(subscriptionFactory));
            MessageHandlerFactory = messageHandlerFactory
                ?? throw new ComponentNotInitialisedException(nameof(messageHandlerFactory));
            _logger = logger;
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="consumerViewModel">View model which should receive subscriptions and then handle them</param>
        public void Initialise(IConsumerViewModel consumerViewModel)
        {
            _logger?.LogInfo("Injecting consumer's view model for ISubscriptionFactory");
            (BindingFactory as ISubscriptionFactory)?.Initialise(consumerViewModel);

            _logger?.LogInfo("Starting communication");
            Start();
        }

        /// <summary>
        /// Starts this instance in asynchronous mode - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="consumerViewModel">View model which should receive subscriptions and then handle them</param>
        public async Task InitialiseAsync(IConsumerViewModel consumerViewModel)
        {
            await Task.Run(() => Initialise(consumerViewModel));
        }

        /// <summary>
        /// Restarts the instance (rereads configuration) 
        /// </summary>
        public void RefreshConfiguration()
        {
            _logger?.LogInfo("Reloading consumer configuration and restarting communication");
            Start();
        }
    }
}
