using CommonServiceLocator;
using M2MCommunication.Core;
using M2MCommunication.Services;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class UaooiServiceLocatorTest
    {
        [Fact]
        public void ConstructorTest()
        {
            AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            );
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeExportedValue(catalog);
            IServiceLocator serviceLocator = Activator.CreateInstance(
                Assembly.GetAssembly(typeof(ServiceContainerSetup))
                    .DefinedTypes
                    .Where(type => type.Name.Equals("UaooiServiceLocator"))
                    .FirstOrDefault(), 
                new[] {container}
            ) as IServiceLocator;
            Assert.NotNull(serviceLocator);
            Assert.Equal(container, serviceLocator.GetType().GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(serviceLocator) as CompositionContainer);
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
                    new[] { (CompositionContainer)null }
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
            AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            );
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeExportedValue(catalog);
            IServiceLocator serviceLocator = Activator.CreateInstance(
                Assembly.GetAssembly(typeof(ServiceContainerSetup))
                    .DefinedTypes
                    .Where(type => type.Name.Equals("UaooiServiceLocator"))
                    .FirstOrDefault(),
                new[] { container }
            ) as IServiceLocator;
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            var tmp = container?.GetExports(typeof(IMessageBus), null, null);
            Assert.NotEmpty(serviceLocator.GetAllInstances(typeof(IMessageBus)));
        }

        [Fact]
        public void DoGetAllInstancesNullTest()
        {
            AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            );
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeExportedValue(catalog);
            IServiceLocator serviceLocator = Activator.CreateInstance(
                Assembly.GetAssembly(typeof(ServiceContainerSetup))
                    .DefinedTypes
                    .Where(type => type.Name.Equals("UaooiServiceLocator"))
                    .FirstOrDefault(),
                new[] { container }
            ) as IServiceLocator;
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            Assert.Throws<NullReferenceException>(() => serviceLocator.GetAllInstances(null));
        }

        [Fact]
        public void DoGetAllInstancesMissingTypeTest()
        {
            AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ServiceContainerSetupTest.Settings.LibraryDirectory)
                )
            );
            CompositionContainer container = new CompositionContainer(catalog);
            container.ComposeExportedValue(catalog);
            IServiceLocator serviceLocator = Activator.CreateInstance(
                Assembly.GetAssembly(typeof(ServiceContainerSetup))
                    .DefinedTypes
                    .Where(type => type.Name.Equals("UaooiServiceLocator"))
                    .FirstOrDefault(),
                new[] { container }
            ) as IServiceLocator;
            ServiceLocator.SetLocatorProvider(() => serviceLocator);
            Assert.Empty(serviceLocator.GetAllInstances(typeof(UaooiServiceLocatorTest)));
        }
    }
}
