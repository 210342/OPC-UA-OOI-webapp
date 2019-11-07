using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace M2MCommunication.Services
{
    class UaooiServiceLocator : ServiceLocatorImplBase
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
    }
}
