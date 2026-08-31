using ReactiveHMI.M2MCommunication.Core.CommonTypes;
using ReactiveHMI.M2MCommunication.Core.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ReactiveInterfaceUnitTest")]
[assembly: InternalsVisibleTo("M2MCommunication.UnitTest")]

namespace ReactiveHMI.M2MCommunication.Services
{
    public class ServiceContainerSetup : IDisposable
    {
        private readonly UaLibrarySettings _uaLibrarySettings;
        private readonly ILogger _logger;
        private ILoggerContainer loggerContainer;

        public ServiceContainerSetup(UaLibrarySettings settings, ILogger logger)
        {
            _uaLibrarySettings = settings;
            _logger = logger;
        }

        public ServiceContainerSetup Initialise()
        {
            _logger?.LogInfo("Initialising Managed Extensibility Framework container");
            AggregateCatalog AggregateCatalog = new(
                new DirectoryCatalog(
                    Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), _uaLibrarySettings.LibraryDirectory)
                )
            );
            CompositionContainer Container = new(AggregateCatalog);

            _logger?.LogInfo("Composing configuration file name and logger instance");
            Container.ComposeExportedValue(UaContractNames.ConfigurationFileNameContract, Path.Combine(
                Directory.GetCurrentDirectory(),
                _uaLibrarySettings.ResourcesDirectory,
                _uaLibrarySettings.LibraryDirectory,
                _uaLibrarySettings.ConsumerConfigurationFile)
            );
            Container.ComposeExportedValue(_logger);

            _logger?.LogInfo("Setting a service locator");
            UaooiServiceLocator.Current = new UaooiServiceLocator(Container);

            if (shouldComposeLoggers)
            {
                _logger?.LogInfo("Composing a common logger for all components");
                loggerContainer = Container.GetExportedValue<ILoggerContainer>().EnableLoggers();
            }

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
                    UaooiServiceLocator.Current?.Dispose();
                    (loggerContainer as IDisposable)?.Dispose();
                }
                UaooiServiceLocator.Current = null;
                loggerContainer = null;
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            GC.SuppressFinalize(this);
            Dispose(true);
        }
        #endregion

        #region Unit test workaround

        private readonly bool shouldComposeLoggers = true;

        #endregion
    }
}
