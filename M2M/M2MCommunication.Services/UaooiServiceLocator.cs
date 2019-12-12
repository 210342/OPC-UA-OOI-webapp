using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace M2MCommunication.Services
{
    internal class UaooiServiceLocator : ServiceLocatorImplBase, IDisposable
    {
        private readonly CompositionContainer _container;

        public UaooiServiceLocator(CompositionContainer compositionContainer)
        {
            _container = compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            return _container?.GetExports(serviceType, null, null)?.Select(e => e.Value) ?? Enumerable.Empty<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            return _container?.GetExports(serviceType, null, key)?.Select(e => e.Value)?.SingleOrDefault();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _container.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
