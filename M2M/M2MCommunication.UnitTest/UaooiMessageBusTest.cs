using CommonServiceLocator;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using ReactiveHMI.M2MCommunication.Services;
using ReactiveHMI.M2MCommunication.UaooiInjections.Components;
using System;
using System.Reflection;
using UAOOI.Configuration.Networking;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData;
using Xunit;

namespace ReactiveHMI.M2MCommunicationUnitTest
{
    [Collection("DI")]
    public class UaooiMessageBusTest
    {
        internal static IConsumerViewModel ConsumerViewModel => new ConsumerVM();

        private class ConsumerVM : IConsumerViewModel
        {
            public void AddSubscription(ISubscription subscription)
            {
            }
        }

        [Fact]
        public void ConstructorTest()
        {
            using (ServiceContainerSetup setup = new ServiceContainerSetup(
                ServiceContainerSetupTest.Settings,
                new ServiceContainerSetupTest.TestLogger()))
            {
                setup.GetType()
                    .GetField("shouldComposeLoggers", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.SetValue(setup, false);
                setup.Initialise();
                IServiceLocator serviceLocator = setup
                    .GetType()
                    .GetProperty("DisposableServiceLocator", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(setup) as IServiceLocator;
                using (UaooiMessageBus bus = new UaooiMessageBus(
                    serviceLocator.GetInstance<IConfigurationFactory>(),
                    serviceLocator.GetInstance<IEncodingFactory>(),
                    serviceLocator.GetInstance<IBindingFactory>(),
                    serviceLocator.GetInstance<IMessageHandlerFactory>(),
                    serviceLocator.GetInstance<ILogger>()
                    ))
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
        }
    }
}
