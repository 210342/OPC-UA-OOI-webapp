using M2MCommunication.Core;
using System;
using System.Threading.Tasks;

namespace MessageParsingUnitTest.Mocks
{
    class TestMessageBusService : IMessageBus
    {

        public void Initialise(Action<object, ISubscription> onSubsctiptionAdded)
        {
        }

        public Task InitialiseAsync(Action<object, ISubscription> onSubsctiptionAdded)
        {
            return Task.Run(() => Initialise(onSubsctiptionAdded));
        }

        public void RefreshConfiguration()
        {
        }
        public void Dispose()
        {
        }
    }
}
