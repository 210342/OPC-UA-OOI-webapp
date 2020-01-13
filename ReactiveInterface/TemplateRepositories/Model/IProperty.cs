using M2MCommunication.Core.Interfaces;

namespace TemplateRepositories.Model
{
    public interface IProperty
    {
        ISubscription Subscription { get; set; }
        IPropertyTemplate Template { get; }
    }
}
