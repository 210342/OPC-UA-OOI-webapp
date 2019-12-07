using CommonServiceLocator;
using M2MCommunication.Core;
using System;

namespace M2MCommunication.Services
{
    public class MessageBusService
    {
        public IMessageBus MessageBus { get; private set; }

        public MessageBusService()
        {
            MessageBus = ServiceLocator.Current.GetInstance<IMessageBus>();
        }
    }
}
