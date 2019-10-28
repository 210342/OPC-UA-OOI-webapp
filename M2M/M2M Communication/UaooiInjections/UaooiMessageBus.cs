using System;
using M2MCommunication.Core;
using UAOOI.Networking.SemanticData;
using CommonServiceLocator;
using UAOOI.Networking.Core;
using M2MCommunication.UaooiInjections;
using System.ComponentModel.Composition;

namespace M2MCommunication.UaaoiInjections
{
    [Export(typeof(IMessageBus))]
    public class UaooiMessageBus : DataManagementSetup, IMessageBus
    {
        public UaooiMessageBus()
        {
            IServiceLocator _serviceLocator = ServiceLocator.Current;
            ConfigurationFactory = new Configuration(UaooiContracts.ConfigurationFileName);
            EncodingFactory = _serviceLocator.GetInstance<IEncodingFactory>();
            BindingFactory = new ConsumerBindingFactory();
            MessageHandlerFactory = _serviceLocator.GetInstance<IMessageHandlerFactory>();
        }

        /// <summary>
        /// Starts this instance - Initializes the data set infrastructure, enable all associations and starts pumping the data;
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public void Initialize()
        {
            Start();
        }
    }
}
