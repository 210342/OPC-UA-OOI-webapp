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
        [ImportingConstructor]
        public UaooiMessageBus(
            IConfigurationFactory configurationFactory,
            IEncodingFactory encodingFactory,
            IBindingFactory subscriptionFactory,
            IMessageHandlerFactory messageHandlerFactory)
        {
            ConfigurationFactory = configurationFactory
                ?? throw new ComponentNotIntialisedException(nameof(configurationFactory));
            EncodingFactory = encodingFactory
                ?? throw new ComponentNotIntialisedException(nameof(encodingFactory));
            BindingFactory = subscriptionFactory
                ?? throw new ComponentNotIntialisedException(nameof(subscriptionFactory));
            MessageHandlerFactory = messageHandlerFactory
                ?? throw new ComponentNotIntialisedException(nameof(messageHandlerFactory));
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        /// <exception cref="ComponentNotIntialisedException"></exception>
        /// <exception cref="ConfigurationFileNotFoundException"></exception>
        /// <exception cref="UnsupportedTypeException"></exception>
        /// <exception cref="ValueRankOutOfRangeException"></exception>
        public void Initialise(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded)
        {
            AssertComponentsAreNotNull();
            (ConfigurationFactory as Configuration)
                ?.Initialise(Path.Combine(Directory.GetCurrentDirectory(), settings.ResourcesDirectory, settings.LibraryDirectory, settings.ConsumerConfigurationFile));
            (BindingFactory as ISubscriptionFactory).Initialise(CommonServiceLocator.ServiceLocator.Current.GetInstance<IConfiguration>());
            (BindingFactory as ISubscriptionFactory).SubscriptionAdded += new EventHandler<ISubscription>(onSubsctiptionAdded);
            Start();
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        /// <exception cref="ComponentNotIntialisedException"></exception>
        /// <exception cref="ConfigurationFileNotFoundException"></exception>
        /// <exception cref="UnsupportedTypeException"></exception>
        /// <exception cref="ValueRankOutOfRangeException"></exception>
        public async Task InitialiseAsync(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded)
        {
            await Task.Run(() => Initialise(settings, onSubsctiptionAdded));
        }

        public void RefreshConfiguration()
        {
            ConfigurationFactory.GetConfiguration();
            (BindingFactory as ISubscriptionFactory).Initialise(CommonServiceLocator.ServiceLocator.Current.GetInstance<IConfiguration>());
            Start();
        }

        private void AssertComponentsAreNotNull()
        {
            if (ConfigurationFactory is null)
            {
                throw new ComponentNotIntialisedException(nameof(ConfigurationFactory));
            }
            if (EncodingFactory is null)
            {
                throw new ComponentNotIntialisedException(nameof(EncodingFactory));
            }
            if (BindingFactory is null)
            {
                throw new ComponentNotIntialisedException(nameof(BindingFactory));
            }
            if (MessageHandlerFactory is null)
            {
                throw new ComponentNotIntialisedException(nameof(MessageHandlerFactory));
            }
        }
    }
}
