using System;
using System.Threading.Tasks;

namespace M2MCommunication.Core
{
    public interface IMessageBus : IDisposable
    {
        void Initialise(IConsumerViewModel consumerViewModel);
        Task InitialiseAsync(IConsumerViewModel consumerViewModel);
        void RefreshConfiguration();
    }
}
