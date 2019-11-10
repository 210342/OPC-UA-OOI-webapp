using CommonServiceLocator;
using M2MCommunication.Core;

namespace M2MCommunication.Services
{
    public class SubscriptionFactoryService
    {
        public ISubscriptionFactory SubscriptionFactory { get; private set; }

        public SubscriptionFactoryService()
        {
            SubscriptionFactory = ServiceLocator.Current.GetInstance<ISubscriptionFactory>();
        }
    }
}
