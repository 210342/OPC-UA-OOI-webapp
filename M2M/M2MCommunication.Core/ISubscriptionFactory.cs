using System.ComponentModel;

namespace M2MCommunication.Core
{
    public interface ISubscriptionFactory
    {
        ISubscription GetSubscription(string subscriptionName, PropertyChangedEventHandler handler);
    }
}
