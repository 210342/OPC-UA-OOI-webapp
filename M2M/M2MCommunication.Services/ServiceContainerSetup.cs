using CommonServiceLocator;
using M2MCommunication.Core;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace M2MCommunication.Services
{
    public class ServiceContainerSetup : IDisposable
    {
        private readonly UaLibrarySettings _uaLibrarySettings;
        private readonly ILogger _logger;

        internal IServiceLocator DisposableServiceLocator { get; private set; }

        public ServiceContainerSetup(UaLibrarySettings settings, ILogger logger)
        {
            _uaLibrarySettings = settings;
            _logger = logger;
        }

        public ServiceContainerSetup Initialise()
        {
            _logger?.LogInfo("Initialising Managed Extensibility Framework container");
            AggregateCatalog AggregateCatalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), _uaLibrarySettings.LibraryDirectory)
                )
            );
            CompositionContainer Container = new CompositionContainer(AggregateCatalog);
            Container.ComposeExportedValue(ContractNames.ConfigurationFileNameContract, Path.Combine(
                Directory.GetCurrentDirectory(), 
                _uaLibrarySettings.ResourcesDirectory, 
                _uaLibrarySettings.LibraryDirectory, 
                _uaLibrarySettings.ConsumerConfigurationFile)
            );
            Container.ComposeExportedValue(_logger);
            DisposableServiceLocator = new UaooiServiceLocator(Container);
            _logger?.LogInfo("Setting a service locator");
            ServiceLocator.SetLocatorProvider(() => DisposableServiceLocator);
            return this;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    (DisposableServiceLocator as IDisposable)?.Dispose();
                }
                DisposableServiceLocator = null;

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
