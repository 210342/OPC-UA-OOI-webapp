using CommonServiceLocator;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using ReactiveHMI.M2MCommunication.Services;
using ReactiveHMI.ReactiveInterfaceUnitTest.Mocks;
using ReactiveHMI.ReferenceWebApplication.ReactiveInterface;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace ReactiveHMI.ReactiveInterfaceUnitTest
{
    public class MessageParserTest
    {
        public MessageParserTest()
        {
            ServiceLocator.SetLocatorProvider(() => new TestServiceLocator());
        }

        protected internal MessageBusService GetMessageBusService()
        {
            MessageBusService service = new MessageBusService();
            service.GetType()
                .GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.Public)
                .SetValue(service, new TestMessageBusService());
            return service;
        }

        [Fact]
        public void ConstructorTest()
        {
            ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), new TestImageTemplateRepository());
            Assert.NotNull(sut.GetType().GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
            Assert.NotNull(sut.PrintableProperties);
            Assert.Empty(sut.PrintableProperties);
        }
    }
}
