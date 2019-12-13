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
            using (ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), new TestImageTemplateRepository()))
            {
                Assert.NotNull(sut.GetType().GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.PrintableProperties);
                Assert.Empty(sut.PrintableProperties);
            }
        }

        [Fact]
        public void DisposeTest()
        {
            ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), new TestImageTemplateRepository());
            sut.Dispose();
            Assert.True(typeof(MessageParser)
                .GetField("disposedValue", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(sut) as bool?);
            Assert.Empty(typeof(MessageParser)
                .GetField("_subscriptions", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(sut) as IEnumerable<ISubscription>);
            Assert.Null(typeof(MessageParser)
                .GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(sut) as IMessageBus);
        }
    }
}
