using InterfaceModel.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReferenceWebApplication.ReactiveInterface
{
    public interface IMessageParser : IDisposable
    {
        IEnumerable<PrintableProperty> PrintableProperties { get; }

        Task InitialiseAsync(Func<Task> handler);
        void RefreshConfiguration();
    }
}
