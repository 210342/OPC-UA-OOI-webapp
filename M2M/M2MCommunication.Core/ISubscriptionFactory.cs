using System.ComponentModel;

namespace M2MCommunication.Core
{
    public interface ISubscriptionFactory
    {
        ISubscription Subscribe(string subscriptionName, PropertyChangedEventHandler handler);
    }
}
