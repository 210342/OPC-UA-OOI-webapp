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
            ConfigurationFactory = configuration as IConfigurationFactory
                ?? throw new ComponentNotIntialisedException(nameof(configuration));
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
        public void Initialise(UaLibrarySettings settings)
        {
            Initialise(settings, ex => throw ex);
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        /// <param name="exceptionHandler">Action used to handle exceptions. 
        /// Handled exceptions: 
        /// <see cref="ConfigurationFileNotFoundException"/>, 
        /// <see cref="ValueRankOutOfRangeException"/>, 
        /// <see cref="UnsupportedTypeException"/>.
        /// </param>
        public void Initialise(UaLibrarySettings settings, Action<Exception> exceptionHandler)
        {
            try
            {
                (ConfigurationFactory as Configuration)
                    ?.Initialise(Path.Combine(Directory.GetCurrentDirectory(), settings.ResourcesDirectory, settings.LibraryDirectory, settings.ConsumerConfigurationFile));
                Start();
            }
            catch (Exception ex) when (ex is ConfigurationFileNotFoundException
                    || ex is ValueRankOutOfRangeException
                    || ex is UnsupportedTypeException)
            {
                exceptionHandler?.Invoke(ex);
            }
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        public async Task InitialiseAsync(UaLibrarySettings settings)
        {
            await Task.Run(() => Initialise(settings));
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enables all associations and starts pumping the data;
        /// </summary>
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        /// <param name="exceptionHandler">Action used to handle exceptions. 
        /// Handled exceptions: 
        /// <see cref="ConfigurationFileNotFoundException"/>, 
        /// <see cref="ValueRankOutOfRangeException"/>, 
        /// <see cref="UnsupportedTypeException"/>.
        /// </param>
        public async Task InitialiseAsync(UaLibrarySettings settings, Action<Exception> exceptionHandler)
        {
            await Task.Run(() => Initialise(settings, exceptionHandler));
        }
    }
}
