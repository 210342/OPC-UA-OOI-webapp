using System;
using System.Collections.Generic;

namespace ReactiveHMI.M2MCommunication.Core.Interfaces
{
    public interface IServiceContainer : IDisposable
    {
        IEnumerable<object> GetAllInstances(Type serviceType);
        object GetInstance(Type serviceType);
        object GetInstance(Type serviceType, string key);
        IEnumerable<T> GetAllInstances<T>() where T: class;
        T GetInstance<T>() where T: class;
        T GetInstance<T>(string key) where T: class;
    }
}
