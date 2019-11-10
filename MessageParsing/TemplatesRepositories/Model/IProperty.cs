using M2MCommunication.Core;

namespace InterfaceModel.Model
{
    public interface IProperty
    {
        ISubscription Subscription { get; set; }
        IPropertyTemplate Template { get; }
    }
}
