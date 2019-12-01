using CommonServiceLocator;
using M2MCommunication.Core;
using System;

namespace M2MCommunication.Services
{
    public class MessageBusService : IDisposable
    {
        public IMessageBus MessageBus { get; private set; }

        public MessageBusService(UaLibrarySettings uaLibrarySettings)
        {
            MessageBus = ServiceLocator.Current.GetInstance<IMessageBus>();
        }

        public void Dispose()
        {
            MessageBus?.Dispose();
        }
    }
}
