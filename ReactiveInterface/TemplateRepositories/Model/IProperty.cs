using ReactiveHMI.M2MCommunication.Core.Interfaces;

namespace ReactiveHMI.TemplateRepositories.Model
{
    public interface IProperty
    {
        ISubscription Subscription { get; set; }
        IPropertyTemplate Template { get; }
    }
}
