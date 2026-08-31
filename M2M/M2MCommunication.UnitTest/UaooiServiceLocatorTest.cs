using ReactiveHMI.M2MCommunication.Services;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using Xunit;

namespace ReactiveHMI.M2MCommunicationUnitTest
{
    [Collection("DI")]
    public class UaooiServiceLocatorTest
    {
        [Fact]
        public void ConstructorTest()
        {
            AggregateCatalog catalog = new(
                new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            );
            using CompositionContainer container = new(catalog);
            container.ComposeExportedValue(catalog);
            var serviceLocator = new UaooiServiceLocator(container);
            Assert.NotNull(serviceLocator);
            Assert.Equal(container, serviceLocator.GetType().GetField("_container", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(serviceLocator) as CompositionContainer);
        }

        [Fact]
        public void ConstructorNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new UaooiServiceLocator(null));
        }

        [Fact]
        public void DoGetAllInstancesNullTest()
        {
            AggregateCatalog catalog = new(
                new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            );
            using CompositionContainer container = new(catalog);
            container.ComposeExportedValue(catalog);
            var serviceLocator = new UaooiServiceLocator(container);
            Assert.Throws<ArgumentNullException>(() => serviceLocator.GetAllInstances(null));
        }

        [Fact]
        public void DoGetAllInstancesMissingTypeTest()
        {
            AggregateCatalog catalog = new(
                new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            );
            using CompositionContainer container = new(catalog);
            container.ComposeExportedValue(catalog);
            var serviceLocator = new UaooiServiceLocator(container);
            Assert.Empty(serviceLocator.GetAllInstances<UaooiServiceLocatorTest>());
        }
    }
}
