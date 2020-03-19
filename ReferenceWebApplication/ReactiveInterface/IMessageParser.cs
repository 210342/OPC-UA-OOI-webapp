using ReactiveHMI.TemplateRepositories.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReactiveHMI.ReferenceWebApplication.ReactiveInterface
{
    public interface IMessageParser
    {
        IEnumerable<PrintableProperty> PrintableProperties { get; }
        event Func<Task> OnSubscriptionUpdated;

        Task InitialiseAsync();
        void RefreshConfiguration();
    }
}
