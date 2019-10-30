using System;
using M2MCommunication.Core;
using UAOOI.Networking.SemanticData;
using CommonServiceLocator;
using UAOOI.Networking.Core;
using M2MCommunication.UaooiInjections;
using System.ComponentModel.Composition;
using System.IO;

namespace M2MCommunication.UaaoiInjections
{
    [Export(typeof(IMessageBus))]
    public class UaooiMessageBus : DataManagementSetup, IMessageBus
    {
        public UaooiMessageBus()
        {
            IServiceLocator _serviceLocator = ServiceLocator.Current;
            ConfigurationFactory = new Configuration();
            EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
            BindingFactory = new ConsumerBindingFactory();
            MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
        }


        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enable all associations and starts pumping the data;
        /// </summary>
        /// <param name="settings">Object containing application settings targeting Unified Architecture library</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Initialise(UaLibrarySettings settings)
        {
            (ConfigurationFactory as Configuration)
                ?.Initialise(Path.Combine(Directory.GetCurrentDirectory(), settings.WebRoot, settings.LibraryDirectory, settings.ConsumerConfigurationFile));
            Start();
        }
    }
}
