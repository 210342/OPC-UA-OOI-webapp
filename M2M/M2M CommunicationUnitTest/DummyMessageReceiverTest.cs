using M2MCommunication;
using System.Collections.Generic;
using Xunit;
using static M2MCommunicationUnitTest.MessageTest;

namespace M2MCommunicationUnitTest
{
    public class DummyMessageReceiverTest
    {
        private IMessageBus _bus = new MessageBus();

        [Fact]
        public void NoSubscriptionsOnCreationTest()
        {
            using (IMessageReceiver receiver = new DummyMessageReceiver(_bus))
            {
                Assert.Empty(receiver.Subscriptions);
            }
        }

        [Fact]
        public void SubscribeInCtorTest()
        {
            IMessageReceiver receiver = 
                new DummyMessageReceiver(_bus, new List<ISubscription>(new[] { new Subscription(TestMessageType.StaticTypeGuid) }), (_) => { });
            Assert.NotEmpty(receiver.Subscriptions);
            receiver.Dispose();
            Assert.Empty(receiver.Subscriptions);
        }

        [Fact]
        public void SubscribeTest()
        {
            IMessageReceiver receiver =
                new DummyMessageReceiver(_bus);
            Assert.Empty(receiver.Subscriptions);
            receiver.Subscribe(new Subscription(TestMessageType.StaticTypeGuid), (_) => { });
            Assert.NotEmpty(receiver.Subscriptions);
            receiver.Dispose();
            Assert.Empty(receiver.Subscriptions);
        }

        [Fact]
        public void UnsubscribeTest()
        {
            using (IMessageReceiver receiver = new DummyMessageReceiver(_bus))
            {
                Assert.Empty(receiver.Subscriptions);
                receiver.Subscribe(new Subscription(TestMessageType.StaticTypeGuid), (_) => { });
                Assert.NotEmpty(receiver.Subscriptions);
                receiver.Unsubscribe(new Subscription(TestMessageType.StaticTypeGuid), (_) => { });
                Assert.Empty(receiver.Subscriptions);
            }
        }
    }
}
