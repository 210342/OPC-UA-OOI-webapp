using System;
using System.Threading.Tasks;

namespace M2MCommunication.Core
{
    public interface IMessageBus : IDisposable
    {
        void Initialise(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded);
        Task InitialiseAsync(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded);
        void RefreshConfiguration();
    }
}
