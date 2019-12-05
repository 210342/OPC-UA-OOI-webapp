using CommonServiceLocator;
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
        [Fact]
        public void ConstructorTest()
        {
            AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            );
            using (CompositionContainer container = new CompositionContainer(catalog))
            {
                container.ComposeExportedValue(catalog);
                IServiceLocator serviceLocator = Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { container }
                ) as IServiceLocator;
                Assert.NotNull(serviceLocator);
                Assert.Equal(container, serviceLocator.GetType().GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(serviceLocator) as CompositionContainer);
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
                    new object[] { null }
                );
            }
            catch (TargetInvocationException ex)
            {
                Assert.IsType<ArgumentNullException>(ex.InnerException);
            }
        }

        [Fact]
        public void DoGetAllInstancesNullTest()
        {
            AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            );
            using (CompositionContainer container = new CompositionContainer(catalog))
            {
                container.ComposeExportedValue(catalog);
                IServiceLocator serviceLocator = Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { container }
                ) as IServiceLocator;
                ServiceLocator.SetLocatorProvider(() => serviceLocator);
                Assert.Throws<NullReferenceException>(() => serviceLocator.GetAllInstances(null));
            }
        }

        [Fact]
        public void DoGetAllInstancesMissingTypeTest()
        {
            AggregateCatalog catalog = new AggregateCatalog(
                new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            );
            using (CompositionContainer container = new CompositionContainer(catalog))
            {
                container.ComposeExportedValue(catalog);
                IServiceLocator serviceLocator = Activator.CreateInstance(
                    Assembly.GetAssembly(typeof(ServiceContainerSetup))
                        .DefinedTypes
                        .Where(type => type.Name.Equals("UaooiServiceLocator"))
                        .FirstOrDefault(),
                    new object[] { container }
                ) as IServiceLocator;
                ServiceLocator.SetLocatorProvider(() => serviceLocator);
                Assert.Empty(serviceLocator.GetAllInstances(typeof(UaooiServiceLocatorTest)));
            }
        }
    }
}
