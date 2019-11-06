using M2MCommunication.Core;

namespace MessageParsing.Model
{
    public abstract class BaseProperty : IProperty
    {
        public ISubscription Subscription { get; set; }
        public IPropertyTemplate Template { get; }

        internal BaseProperty(ISubscription subsctiption, IPropertyTemplate template)
        {
            Subscription = subsctiption;
            Template = template;
        }
    }
}
