using CommonServiceLocator;
using InterfaceModel.Model;
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
        public MessageParserTest()
        {
            ServiceLocator.SetLocatorProvider(() => new TestServiceLocator());
        }

        protected internal ConfigurationService GetTestConfigurationService()
        {
            ConfigurationService service = new ConfigurationService();
            service.GetType()
                .GetProperty("Configuration", BindingFlags.Instance | BindingFlags.Public)
                .SetValue(service, new TestConfigurationService());
            return service;
        }

        protected internal SubscriptionFactoryService GetTestSubscriptionService()
        {
            SubscriptionFactoryService service = new SubscriptionFactoryService();
            service.GetType()
                .GetProperty("SubscriptionFactory", BindingFlags.Instance | BindingFlags.Public)
                .SetValue(service, new TestSubscriptionService());
            return service;
        }

        [Fact]
        public void ConstructorTest()
        {
            using (MessageParser sut = new TextMessageParser(GetTestConfigurationService(), GetTestSubscriptionService()))
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
            using (MessageParser sut = new TextMessageParser(GetTestConfigurationService(), GetTestSubscriptionService()))
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
            MessageParser sut = new TextMessageParser(GetTestConfigurationService(), GetTestSubscriptionService());
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
