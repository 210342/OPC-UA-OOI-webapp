using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;

namespace M2MCommunication.Services
{
    class UaooiServiceLocator : ServiceLocatorImplBase
    {
        private readonly CompositionContainer _container;

        public UaooiServiceLocator(CompositionContainer compositionContainer)
        {
            _container = compositionContainer;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _container?.GetExports(serviceType, null, null)?.Select(e => e.Value) ?? Enumerable.Empty<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _container?.GetExports(serviceType, null, key)?.Select(e => e.Value)?.SingleOrDefault();
        }
    }
}
