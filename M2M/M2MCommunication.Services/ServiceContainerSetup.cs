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
        internal AggregateCatalog AggregateCatalog { get; private set; }
        internal CompositionContainer Container { get; private set; }
        internal IServiceLocator DisposableServiceLocator { get; private set; }

        public ServiceContainerSetup(UaLibrarySettings settings)
        {
            _uaLibrarySettings = settings;
        }

        public ServiceContainerSetup Initialise()
        {
            AggregateCatalog = new AggregateCatalog(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), _uaLibrarySettings.LibraryDirectory)
                )
            );
            Container = new CompositionContainer(AggregateCatalog);
            Container.ComposeExportedValue(AggregateCatalog);
            DisposableServiceLocator = new UaooiServiceLocator(Container);
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
                    Container?.Dispose();
                    AggregateCatalog?.Dispose();
                    (DisposableServiceLocator as UaooiServiceLocator)?.Dispose();
                }
                Container = null;
                AggregateCatalog = null;
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
