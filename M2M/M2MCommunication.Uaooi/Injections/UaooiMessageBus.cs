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
            IConfiguration configuration,
            IEncodingFactory encodingFactory,
            ISubscriptionFactory subscriptionFactory,
            IMessageHandlerFactory messageHandlerFactory)
        {
            ConfigurationFactory = configuration as IConfigurationFactory;
            EncodingFactory = encodingFactory
                ?? throw new ComponentNotIntialisedException(nameof(encodingFactory));
            BindingFactory = subscriptionFactory as IBindingFactory
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
        public void Initialise(UaLibrarySettings settings)
        {
            AssertComponentsAreNotNull();
            (ConfigurationFactory as Configuration)
                ?.Initialise(Path.Combine(Directory.GetCurrentDirectory(), settings.ResourcesDirectory, settings.LibraryDirectory, settings.ConsumerConfigurationFile));
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
        public async Task InitialiseAsync(UaLibrarySettings settings)
        {
            await Task.Run(() => Initialise(settings));
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
