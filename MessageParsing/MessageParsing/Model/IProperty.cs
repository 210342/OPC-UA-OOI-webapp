using M2MCommunication.Core;

namespace MessageParsing.Model
{
    public interface IProperty
    {
        ISubscription Subscription { get; set; }
        IPropertyTemplate Template { get; }
    }
}
