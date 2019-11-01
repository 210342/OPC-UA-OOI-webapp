using CommonServiceLocator;
using M2MCommunication.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace M2MCommunication.Services
{
    public class SubscriptionFactoryService
    {
        public ISubscriptionFactory SubscriptionFactory { get; }

        public SubscriptionFactoryService()
        {
            SubscriptionFactory = ServiceLocator.Current.GetInstance<ISubscriptionFactory>();
        }
    }
}
