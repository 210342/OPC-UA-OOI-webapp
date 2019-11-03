using System;
using System.Threading.Tasks;

namespace M2MCommunication.Core
{
    public interface IMessageBus : IDisposable
    {
        void Initialise(UaLibrarySettings settings);
        void Initialise(UaLibrarySettings settings, Action<Exception> exceptionHandler);
        Task InitialiseAsync(UaLibrarySettings settings);
        Task InitialiseAsync(UaLibrarySettings settings, Action<Exception> exceptionHandler);
    }
}
