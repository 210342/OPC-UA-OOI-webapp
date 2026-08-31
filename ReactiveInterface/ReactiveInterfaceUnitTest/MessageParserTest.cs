using ReactiveHMI.M2MCommunication.Services;
using ReactiveHMI.ReactiveInterface.ReactiveInterfaceUnitTest.Mocks;
using ReactiveHMI.ReferenceWebApplication.ReactiveInterface;
using System.Reflection;
using Xunit;

namespace ReactiveHMI.ReactiveInterface.ReactiveInterfaceUnitTest
{
    public class MessageParserTest
    {
        public MessageParserTest()
        {
            UaooiServiceLocator.Current = new TestServiceLocator();
        }

        protected internal MessageBusService GetMessageBusService()
        {
            MessageBusService service = new();
            service.GetType()
                .GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.Public)
                .SetValue(service, new TestMessageBusService());
            return service;
        }

        [Fact]
        public void ConstructorTest()
        {
            ImageMessageParser sut = new(GetMessageBusService(), new TestImageTemplateRepository());
            Assert.NotNull(sut.GetType().GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
            Assert.NotNull(sut.PrintableProperties);
            Assert.Empty(sut.PrintableProperties);
        }
    }
}
