using CommonServiceLocator;
using M2MCommunication.Core;
using M2MCommunication.Services;
using MessageParsing;
using MessageParsingUnitTest.Mocks;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace MessageParsingUnitTest
{
    public class MessageParserTest
    {
        protected internal UaLibrarySettings Settings => new UaLibrarySettings()
        {
            ResourcesDirectory = "dir",
            ConsumerConfigurationFile = "file",
            LibraryDirectory = "lib"
        };

        public MessageParserTest()
        {
            ServiceLocator.SetLocatorProvider(() => new TestServiceLocator());
        }

        protected internal MessageBusService GetMessageBusService()
        {
            MessageBusService service = new MessageBusService(Settings);
            service.GetType()
                .GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.Public)
                .SetValue(service, new TestMessageBusService());
            return service;
        }

        [Fact]
        public void ConstructorTest()
        {
            using (ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), Settings, new TestImageTemplateRepository()))
            {
                Assert.NotNull(sut.GetType().GetProperty("Configuration", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.GetType().GetProperty("SubscriptionFactory", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.PrintableProperties);
                Assert.Empty(sut.PrintableProperties);
            }
        }

        [Fact]
        public void SubscribeTest()
        {
            using (ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), Settings, new TestImageTemplateRepository()))
            {
                sut.GetType()
                    .GetMethod("Subscribe", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(sut, new object[] { new Func<Task>(async () => await Task.Run(() => { })) });
                Assert.NotNull(typeof(MessageParser)
                    .GetField("_subscriptions", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(sut) as IEnumerable<ISubscription>);
                Assert.NotEmpty(typeof(MessageParser)
                    .GetField("_subscriptions", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(sut) as IEnumerable<ISubscription>);
            }
        }

        [Fact]
        public void DisposeTest()
        {
            ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), Settings, new TestImageTemplateRepository());
            sut.Dispose();
            Assert.True(typeof(MessageParser)
                .GetField("disposedValue", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(sut) as bool?);
            Assert.Empty(typeof(MessageParser)
                .GetField("_subscriptions", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(sut) as IEnumerable<ISubscription>);
        }
    }
}
