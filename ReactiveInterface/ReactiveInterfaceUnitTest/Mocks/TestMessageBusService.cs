using ReactiveHMI.M2MCommunication.Core.Interfaces;
using System.Threading.Tasks;

namespace ReactiveHMI.ReactiveInterfaceUnitTest.Mocks
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
