using M2M_Communication;
using System.Collections.Generic;
using Xunit;
using static M2M_CommunicationUnitTest.MessageTest;

namespace M2M_CommunicationUnitTest
{
    public class DummyMessageReceiverTest
    {
        private IMessageBus _bus = new MessageBus();

        [Fact]
        public void NoSubscriptionsOnCreationTest()
        {
            using IMessageReceiver receiver = new DummyMessageReceiver(_bus, null);
            Assert.Empty(receiver.Subscriptions);
        }

        [Fact]
        public void SubscribeInCtorTest()
        {
            IMessageReceiver receiver = 
                new DummyMessageReceiver(_bus, null, new List<ISubscription>(new[] { new Subscription(TestMessageType.StaticTypeGuid) }));
            Assert.NotEmpty(receiver.Subscriptions);
            receiver.Dispose();
            Assert.Empty(receiver.Subscriptions);
        }

        [Fact]
        public void SubscribeTest()
        {
            IMessageReceiver receiver =
                new DummyMessageReceiver(_bus, null);
            Assert.Empty(receiver.Subscriptions);
            receiver.Subscribe(new Subscription(TestMessageType.StaticTypeGuid));
            Assert.NotEmpty(receiver.Subscriptions);
            receiver.Dispose();
            Assert.Empty(receiver.Subscriptions);
        }

        [Fact]
        public void UnsubscribeTest()
        {
            using (IMessageReceiver receiver = new DummyMessageReceiver(_bus, null))
            {
                Assert.Empty(receiver.Subscriptions);
                receiver.Subscribe(new Subscription(TestMessageType.StaticTypeGuid));
                Assert.NotEmpty(receiver.Subscriptions);
                receiver.Unsubscribe(new Subscription(TestMessageType.StaticTypeGuid));
                Assert.Empty(receiver.Subscriptions);
            }
        }
    }
}
