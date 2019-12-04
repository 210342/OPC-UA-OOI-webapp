using CommonServiceLocator;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace M2MCommunicationUnitTest
{
    [Collection("DI")]
    public class UaooiServiceLocatorTest
    {
        internal ILogger Logger => new ServiceContainerSetupTest.TestLogger();

        [Fact]
        public void ConstructorTest()
        {
            using (AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            ))
            using (CompositionContainer container = new CompositionContainer(catalog))
            {
                container.ComposeExportedValue(catalog);
                IServiceLocator serviceLocator = Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { container, Logger }
                ) as IServiceLocator;
                Assert.NotNull(serviceLocator);
                Assert.NotEqual(container, serviceLocator.GetType().GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(serviceLocator) as CompositionContainer);
            }
        }

        [Fact]
        public void ConstructorNullTest()
        {
            try
            {
                Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { (CompositionContainer)null, null }
                );
            }
            catch (TargetInvocationException ex)
            {
                Assert.IsType<ArgumentNullException>(ex.InnerException);
            }
        }

        [Fact]
        public void DoGetAllInstancesTest()
        {
            using (AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            ))
            using (CompositionContainer container = new CompositionContainer(catalog))
            {
                container.ComposeExportedValue(catalog);
                IServiceLocator serviceLocator = Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { container, Logger }
                ) as IServiceLocator;
                ServiceLocator.SetLocatorProvider(() => serviceLocator);
                System.Collections.Generic.IEnumerable<Lazy<object, object>> tmp = container?.GetExports(typeof(IMessageBus), null, null);
                Assert.NotEmpty(serviceLocator.GetAllInstances(typeof(IMessageBus)));
            }
        }

        [Fact]
        public void DoGetAllInstancesNullTest()
        {
            using (AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            ))
            using (CompositionContainer container = new CompositionContainer(catalog))
            {
                container.ComposeExportedValue(catalog);
                IServiceLocator serviceLocator = Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { container, Logger }
                ) as IServiceLocator;
                ServiceLocator.SetLocatorProvider(() => serviceLocator);
                Assert.Throws<NullReferenceException>(() => serviceLocator.GetAllInstances(null));
            }
        }

        [Fact]
        public void DoGetAllInstancesMissingTypeTest()
        {
            using (AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            ))
            using (CompositionContainer container = new CompositionContainer(catalog))
            {
                container.ComposeExportedValue(catalog);
                IServiceLocator serviceLocator = Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { container, Logger }
                ) as IServiceLocator;
                ServiceLocator.SetLocatorProvider(() => serviceLocator);
                Assert.Empty(serviceLocator.GetAllInstances(typeof(UaooiServiceLocatorTest)));
            }
        }
    }
}
