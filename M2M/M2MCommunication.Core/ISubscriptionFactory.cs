using System.ComponentModel;

namespace M2MCommunication.Core
{
    public interface ISubscriptionFactory
    {
        ISubscription Subscribe(UaTypeMetadata uaTypeMetadata, PropertyChangedEventHandler handler);
    }
}
