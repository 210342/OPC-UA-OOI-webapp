using M2MCommunication.Core;
using M2MCommunication.Core.Exceptions;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using UAOOI.Configuration.Networking;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData;

namespace M2MCommunication.Uaooi.Injections
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
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        /// <exception cref="ComponentNotInitialisedException"></exception>
        /// <exception cref="ConfigurationFileNotFoundException"></exception>
        /// <exception cref="UnsupportedTypeException"></exception>
        /// <exception cref="ValueRankOutOfRangeException"></exception>
        public void Initialise(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded)
        {
            AssertComponentsAreNotNull();

            _logger?.LogInfo("Reading configuration");

            (ConfigurationFactory as Configuration)
                ?.Initialise(Path.Combine(Directory.GetCurrentDirectory(), settings.ResourcesDirectory, settings.LibraryDirectory, settings.ConsumerConfigurationFile));

            _logger?.LogInfo("Initialising subscription logic");

            (BindingFactory as ISubscriptionFactory).Initialise(CommonServiceLocator.ServiceLocator.Current.GetInstance<IConfiguration>());
            (BindingFactory as ISubscriptionFactory).SubscriptionAdded += new EventHandler<ISubscription>((sender, subscription) =>
            {
                _logger?.LogInfo($"Adding a subscription for: {subscription.UaTypeMetadata.ToString()}");
                onSubsctiptionAdded(sender, subscription);
            });

            _logger?.LogInfo("Starting communication");

            Start();
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        /// <exception cref="ComponentNotInitialisedException"></exception>
        /// <exception cref="ConfigurationFileNotFoundException"></exception>
        /// <exception cref="UnsupportedTypeException"></exception>
        /// <exception cref="ValueRankOutOfRangeException"></exception>
        public async Task InitialiseAsync(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded)
        {
            await Task.Run(() => Initialise(settings, onSubsctiptionAdded));
        }

        public void RefreshConfiguration()
        {
            _logger?.LogInfo("Reloading consumer configuration");

            ConfigurationFactory.GetConfiguration();
            (BindingFactory as ISubscriptionFactory).Initialise(CommonServiceLocator.ServiceLocator.Current.GetInstance<IConfiguration>());

            _logger?.LogInfo("Consumer configuration reloaded, starting communication");

            Start();
        }

        private void AssertComponentsAreNotNull()
        {
            if (ConfigurationFactory is null)
            {
                var exception = new ComponentNotInitialisedException(nameof(ConfigurationFactory));
                _logger?.LogError(exception, exception.Message);
                throw exception;
            }
            else if (EncodingFactory is null)
            {
                var exception = new ComponentNotInitialisedException(nameof(EncodingFactory));
                _logger?.LogError(exception, exception.Message);
                throw exception;
            }
            else if (BindingFactory is null)
            {
                var exception = new ComponentNotInitialisedException(nameof(BindingFactory));
                _logger?.LogError(exception, exception.Message);
                throw exception;
            }
            else if (MessageHandlerFactory is null)
            {
                var exception = new ComponentNotInitialisedException(nameof(MessageHandlerFactory));
                _logger?.LogError(exception, exception.Message);
                throw exception;
            }
        }
    }
}
