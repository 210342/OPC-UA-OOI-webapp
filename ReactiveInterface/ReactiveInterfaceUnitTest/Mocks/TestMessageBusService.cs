using M2MCommunication.Core;
using System;
using System.Threading.Tasks;

namespace MessageParsingUnitTest.Mocks
{
    class TestMessageBusService : IMessageBus
    {

        public void Initialise(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded)
        {
        }

        public Task InitialiseAsync(UaLibrarySettings settings, Action<object, ISubscription> onSubsctiptionAdded)
        {
            return Task.Run(() => Initialise(settings, onSubsctiptionAdded));
        }

        public void RefreshConfiguration()
        {
        }
        public void Dispose()
        {
        }
    }
}
