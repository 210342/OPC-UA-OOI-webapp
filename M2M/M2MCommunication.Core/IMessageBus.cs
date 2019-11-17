using System;
using System.Threading.Tasks;

namespace M2MCommunication.Core
{
    public interface IMessageBus : IDisposable
    {
        void Initialise(UaLibrarySettings settings);
        Task InitialiseAsync(UaLibrarySettings settings);
    }
}
