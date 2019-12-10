using M2MCommunication.Core;
using System;
using System.Threading.Tasks;

namespace MessageParsingUnitTest.Mocks
{
    class TestMessageBusService : IMessageBus
    {

        public void Initialise(IConsumerViewModel viewModel)
        {
        }

        public Task InitialiseAsync(IConsumerViewModel viewModel)
        {
            return Task.Run(() => Initialise(viewModel));
        }

        public void RefreshConfiguration()
        {
        }
        public void Dispose()
        {
        }
    }
}
