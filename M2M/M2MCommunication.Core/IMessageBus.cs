using System;
using System.Threading.Tasks;

namespace M2MCommunication.Core
{
    public interface IMessageBus : IDisposable
    {
        void Initialise(Action<object, ISubscription> onSubsctiptionAdded);
        Task InitialiseAsync(Action<object, ISubscription> onSubsctiptionAdded);
        void RefreshConfiguration();
    }
}
