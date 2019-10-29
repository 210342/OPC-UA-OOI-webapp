using CommonServiceLocator;
using M2MCommunication.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication.Services
{
    public class MessageBusService : IDisposable
    {
        public IMessageBus MessageBus { get; }

        public MessageBusService()
        {
            MessageBus = ServiceLocator.Current.GetInstance<IMessageBus>();
        }

        public void Dispose()
        {
            MessageBus?.Dispose();
        }
    }
}
