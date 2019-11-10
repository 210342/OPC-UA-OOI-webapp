using M2MCommunication.Core;
using System.ComponentModel;

namespace MessageParsingUnitTest.Mocks
{
    internal class TestSubscriptionService : ISubscriptionFactory
    {
        public ISubscription Subscribe(string subscriptionName, PropertyChangedEventHandler handler)
        {
            TestSubscription subscription = new TestSubscription
            {
                TypeName = subscriptionName
            };
            subscription.Enable(handler);
            return subscription;
        }
    }
}
