using M2MCommunication.Core;
using M2MCommunication.Core.Exceptions;
using M2MCommunication.Uaooi.Injections;
using System;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData.DataRepository;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class ConsumerBindingFactoryTest
    {
        private static readonly string repositoryGroup = "repository group";
        private static readonly string typeName = "type name";

        [Fact]
        public void GetProducerBindingTest()
        {
            Assert.Throws<NotSupportedException>(() =>
                new ConsumerBindingFactory().GetProducerBinding(string.Empty, string.Empty, null));
        }

        [Fact]
        public void GetConsumerBindingTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, -1, new int[] { });
            IConsumerBinding binding = factory.GetConsumerBinding(repositoryGroup, typeName, typeInfo);
            Assert.NotNull(binding);
            Assert.True(factory.Subscriptions.ContainsKey(typeName));
            Assert.NotNull(factory.Subscriptions[typeName]);
        }

        [Fact]
        public void GetConsumerBindingForNullTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            Assert.Throws<ArgumentNullException>(() => factory.GetConsumerBinding(repositoryGroup, typeName, null));
        }

        [Fact]
        public void GetConsumerBindingForMultidimensionalTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, 2, new int[] { 21, 37 });
            Assert.Throws<ValueRankOutOfRangeException>(() => factory.GetConsumerBinding(repositoryGroup, typeName, typeInfo));
        }

        [Fact]
        public void GetConsumerBindingForNotSupportedTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.NodeId, -2, new int[] { });
            Assert.Throws<UnsupportedTypeException>(() => factory.GetConsumerBinding(repositoryGroup, typeName, typeInfo));
        }

        [Fact]
        public void SubscribeNotBoundTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            Assert.Throws<UnsupportedTypeException>(() => factory.Subscribe(typeName, (sender, args) => { }));
        }

        [Fact]
        public void SubscribeBoundTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, -1, new int[] { });
            IConsumerBinding binding = factory.GetConsumerBinding(repositoryGroup, typeName, typeInfo);
            ISubscription subscription = factory.Subscribe(typeName, (sender, args) => { });
            Assert.NotNull(subscription);
            Assert.Equal(typeName, subscription.TypeName);
            Assert.Equal(binding, subscription.Value);
        }

        [Fact]
        public void SubscriptionPropertyChangedEventHandlerTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, -1, new int[] { });
            IConsumerBinding binding = factory.GetConsumerBinding(repositoryGroup, typeName, typeInfo);
            bool invoked = false;
            ISubscription subscription = factory.Subscribe(typeName, (sender, args) => invoked = true);
            subscription.Value = null;
            Assert.True(invoked);
        }
    }
}
