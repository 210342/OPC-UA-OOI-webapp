﻿using CommonServiceLocator;
using M2MCommunication.Core;
using M2MCommunication.Services;
using M2MCommunication.Uaooi.Injections;
using System.Reflection;
using UAOOI.Networking.Core;
using UAOOI.Networking.SemanticData;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class UaooiMessageBusTest
    {
        [Fact]
        public void ConstructorTest()
        {
            using (ServiceContainerSetup setup = new ServiceContainerSetup(ServiceContainerSetupTest.Settings))
            {
                setup.Initialise();
                IServiceLocator serviceLocator = setup
                    .GetType()
                    .GetProperty("DisposableServiceLocator", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(setup) as IServiceLocator;
                using (UaooiMessageBus bus = new UaooiMessageBus(
                    serviceLocator.GetInstance<IConfiguration>(),
                    serviceLocator.GetInstance<IEncodingFactory>(),
                    serviceLocator.GetInstance<ISubscriptionFactory>(),
                    serviceLocator.GetInstance<IMessageHandlerFactory>()
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

        [Fact]
        public void InitialiseTest()
        {
            using (ServiceContainerSetup setup = new ServiceContainerSetup(ServiceContainerSetupTest.Settings))
            {
                setup.Initialise();
                IServiceLocator serviceLocator = setup
                    .GetType()
                    .GetProperty("DisposableServiceLocator", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(setup) as IServiceLocator;
                using (UaooiMessageBus bus = new UaooiMessageBus(
                    serviceLocator.GetInstance<IConfiguration>(),
                    serviceLocator.GetInstance<IEncodingFactory>(),
                    serviceLocator.GetInstance<ISubscriptionFactory>(),
                    serviceLocator.GetInstance<IMessageHandlerFactory>()
                ))
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
}