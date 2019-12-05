using System;

namespace M2MCommunication.Core
{
    public interface ISubscriptionFactory
    {
        event EventHandler<ISubscription> SubscriptionAdded;
    }
}
