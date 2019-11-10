using CommonServiceLocator;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class ServiceContainerSetupTest
    {
        internal static UaLibrarySettings Settings => new UaLibrarySettings()
        {
            ResourcesDirectory = @"..\..\..\..\..\ReferenceWebApplication\wwwroot",
            LibraryDirectory = "M2MCommunication.Uaooi",
            ConsumerConfigurationFile = "ConfigurationDataConsumer.xml"
        };

        [Fact]
        public void ConstructorTest()
        {
            using (ServiceContainerSetup setup = new ServiceContainerSetup(Settings))
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
            using (ServiceContainerSetup setup = new ServiceContainerSetup(Settings))
            {
                setup.Initialise();
                AggregateCatalog catalog = setup.GetType().GetProperty("AggregateCatalog", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(setup) as AggregateCatalog;
                Assert.NotNull(catalog);
                Assert.NotNull(setup
                    .GetType()
                    .GetProperty("Container", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(setup));
                Assert.NotNull(ServiceLocator.Current);
                Assert.IsType(
                    Assembly
                        .GetAssembly(setup.GetType())
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    ServiceLocator.Current);
                Assert.NotEmpty(catalog);
            }
        }

        [Fact]
        public void DisposeTest()
        {
            ServiceContainerSetup setup = new ServiceContainerSetup(Settings);
            setup.Dispose();
            Assert.True(setup.GetType().GetField("disposedValue", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(setup) as bool?);
        }
    }
}
