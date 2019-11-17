using M2MCommunication.Core;
using System.ComponentModel;

namespace MessageParsingUnitTest.Mocks
{
    internal class TestSubscriptionService : ISubscriptionFactory
    {
        public ISubscription Subscribe(UaTypeMetadata uaTypeMetadata, PropertyChangedEventHandler handler)
        {
            TestSubscription subscription = new TestSubscription
            {
                UaTypeMetadata = uaTypeMetadata
            };
            subscription.Enable(handler);
            return subscription;
        }
    }
}
