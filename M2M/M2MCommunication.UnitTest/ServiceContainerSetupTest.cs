using CommonServiceLocator;
using ReactiveHMI.M2MCommunication.Core.CommonTypes;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using ReactiveHMI.M2MCommunication.Services;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace ReactiveHMI.M2MCommunicationUnitTest
{
    public class ServiceContainerSetupTest
    {
        internal static UaLibrarySettings Settings => new UaLibrarySettings()
        {
            ResourcesDirectory = @"..\..\..\..\..\ReferenceWebApplication\wwwroot",
            LibraryDirectory = "M2MCommunication.Uaooi",
            ConsumerConfigurationFile = "ConfigurationDataConsumer.xml"
        };

        internal class TestLogger : ILogger
        {

            public void LogError(Exception exception, string message, string callerName = "", string callerPath = "")
            {
            }

            public void LogError(string message, string callerName = "", string callerPath = "")
            {
            }

            public void LogInfo(string message, string callerName = "", string callerPath = "")
            {
            }

            public void LogWarning(Exception exception, string message, string callerName = "", string callerPath = "")
            {
            }

            public void LogWarning(string message, string callerName = "", string callerPath = "")
            {
            }
        }

        [Fact]
        public void ConstructorTest()
        {
            using (ServiceContainerSetup setup = new ServiceContainerSetup(Settings, new TestLogger()))
            {
                Assert.Equal(Settings, setup.GetType()
                    .GetField("_uaLibrarySettings", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(setup)
                );
                Assert.True(Directory.Exists(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    Settings.ResourcesDirectory,
                    Settings.LibraryDirectory
                )));
                Assert.True(File.Exists(Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    Settings.ResourcesDirectory,
                    Settings.LibraryDirectory,
                    Settings.ConsumerConfigurationFile
                )));
            }
        }

        [Fact]
        public void InitialiseTest()
        {
            using (ServiceContainerSetup setup = new ServiceContainerSetup(Settings, new TestLogger()))
            {
                setup.Initialise();
                Assert.NotNull(setup
                    .GetType()
                    .GetProperty("DisposableServiceLocator", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(setup));
                Assert.NotNull(ServiceLocator.Current);
                Assert.IsType(
                    Assembly
                        .GetAssembly(setup.GetType())
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    ServiceLocator.Current);
            }
        }

        [Fact]
        public void DisposeTest()
        {
            ServiceContainerSetup setup = new ServiceContainerSetup(Settings, new TestLogger());
            setup.Dispose();
            Assert.True(setup.GetType().GetField("disposedValue", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(setup) as bool?);
        }
    }
}
