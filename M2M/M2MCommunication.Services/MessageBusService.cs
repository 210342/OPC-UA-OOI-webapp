using CommonServiceLocator;
using M2MCommunication.Core;
using System;

namespace M2MCommunication.Services
{
    public class MessageBusService : IDisposable
    {
        public IMessageBus MessageBus { get; }

        public MessageBusService(UaLibrarySettings uaLibrarySettings)
        {
            MessageBus = ServiceLocator.Current.GetInstance<IMessageBus>();
            MessageBus?.Initialise(uaLibrarySettings);
        }

        public void Dispose()
        {
            MessageBus?.Dispose();
        }
    }
}
