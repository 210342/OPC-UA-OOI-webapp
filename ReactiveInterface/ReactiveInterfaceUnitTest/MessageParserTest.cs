using CommonServiceLocator;
using M2MCommunication.Core.Interfaces;
using M2MCommunication.Services;
using ReactiveInterfaceUnitTest.Mocks;
using ReferenceWebApplication.ReactiveInterface;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace ReactiveInterfaceUnitTest
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
