using CommonServiceLocator;
using ReactiveHMI.M2MCommunication.Core.Interfaces;

namespace ReactiveHMI.M2MCommunication.Services
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
