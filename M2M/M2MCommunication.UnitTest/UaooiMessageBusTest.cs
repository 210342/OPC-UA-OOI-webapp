using M2MCommunication.Services;
using M2MCommunication.Uaooi.Injections;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace M2MCommunicationUnitTest
{
    [Collection("DI")]
    public class UaooiMessageBusTest : IDisposable
    {
        private readonly ServiceContainerSetup _setup;

        public UaooiMessageBusTest()
        {
            _setup = new ServiceContainerSetup(ServiceContainerSetupTest.Settings);
        }

        public void Dispose()
        {
            _setup.Dispose();
        }

        [Fact]
        public void ConstructorTest()
        {
            _setup.Initialise();
            using (UaooiMessageBus bus = new UaooiMessageBus())
            {
                Assert.NotNull(bus.ConfigurationFactory);
                Assert.NotNull(bus.EncodingFactory);
                Assert.NotNull(bus.BindingFactory);
                Assert.NotNull(bus.MessageHandlerFactory);
                Assert.Null(bus
                    .GetType()
                    .GetProperty("AssociationsCollection", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(bus));
                Assert.Null(bus
                    .GetType()
                    .GetProperty("MessageHandlersCollection", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(bus));
            }
        }

        [Fact]
        public void ConstructorWithoutServiceLocatorTest()
        {
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => null);
            Assert.Throws<InvalidOperationException>(() => new UaooiMessageBus());
        }

        [Fact]
        public void InitialiseTest()
        {
            _setup.Initialise();
            using (UaooiMessageBus bus = new UaooiMessageBus())
            {
                bus.Initialise(ServiceContainerSetupTest.Settings);
                Assert.NotNull(bus.ConfigurationFactory);
                Assert.NotNull(bus.EncodingFactory);
                Assert.NotNull(bus.BindingFactory);
                Assert.NotNull(bus.MessageHandlerFactory);
                Assert.NotNull(bus
                    .GetType()
                    .GetProperty("AssociationsCollection", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(bus));
                Assert.NotNull(bus
                    .GetType()
                    .GetProperty("MessageHandlersCollection", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(bus));
            }
        }
    }
}
