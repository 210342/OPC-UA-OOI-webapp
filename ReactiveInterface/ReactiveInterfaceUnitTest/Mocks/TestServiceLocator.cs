using System;
using System.Collections.Generic;
using ReactiveHMI.M2MCommunication.Core.Interfaces;

namespace ReactiveHMI.ReactiveInterface.ReactiveInterfaceUnitTest.Mocks
{
    internal class TestServiceLocator : IServiceContainer
    {
        private bool disposedValue;

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return [];
        }

        public IEnumerable<T> GetAllInstances<T>() where T : class
        {
            return [];
        }

        public object GetInstance(Type serviceType)
        {
            return null;
        }

        public object GetInstance(Type serviceType, string key)
        {
            return null;
        }

        public T GetInstance<T>() where T : class
        {
            return null;
        }

        public T GetInstance<T>(string key) where T : class
        {
            return null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
