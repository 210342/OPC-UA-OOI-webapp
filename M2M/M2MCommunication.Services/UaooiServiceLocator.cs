using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using ReactiveHMI.M2MCommunication.Core.Interfaces;

namespace ReactiveHMI.M2MCommunication.Services
{
    public class UaooiServiceLocator : IServiceContainer
    {
        private readonly CompositionContainer _container;

        public static IServiceContainer Current { get; internal set; }

        internal UaooiServiceLocator(CompositionContainer compositionContainer)
        {
            _container = compositionContainer ?? throw new ArgumentNullException(nameof(compositionContainer));
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            ArgumentNullException.ThrowIfNull(serviceType);
            if (disposedValue)
            {
                return [];
            }
            return _container?.GetExports(serviceType, null, null)?.Select(e => e.Value) ?? [];
        }

        public object GetInstance(Type serviceType)
        {
            return GetInstance(serviceType, null);
        }

        public object GetInstance(Type serviceType, string key)
        {
            ArgumentNullException.ThrowIfNull(serviceType);
            if (disposedValue)
            {
                return null;
            }
            return _container?.GetExports(serviceType, null, key)?.Select(e => e.Value)?.SingleOrDefault();
        }

        public IEnumerable<T> GetAllInstances<T>()
            where T: class
        {
            if (disposedValue)
            {
                return [];
            }
            return _container?.GetExports(typeof(T), null, null)?.Select(e => e.Value as T) ?? [];
        }

        public T GetInstance<T>()
            where T: class
        {
            return GetInstance<T>(null);
        }

        public T GetInstance<T>(string key)
            where T: class
        {
            if (disposedValue)
            {
                return null;
            }
            return _container?.GetExports(typeof(T), null, key)?.Select(e => e.Value as T)?.SingleOrDefault();
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
