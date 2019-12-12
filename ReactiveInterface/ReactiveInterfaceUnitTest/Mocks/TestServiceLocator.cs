using CommonServiceLocator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactiveInterfaceUnitTest.Mocks
{
    internal class TestServiceLocator : ServiceLocatorImplBase
    {
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return null;
        }
    }
}
