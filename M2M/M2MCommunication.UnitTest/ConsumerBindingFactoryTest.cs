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
        private static readonly UaTypeMetadata _typeMetadata = new UaTypeMetadata("repository group", "type name");

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
            IConsumerBinding binding = factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo);
            Assert.NotNull(binding);
            Assert.True(factory.Subscriptions.ContainsKey(_typeMetadata));
            Assert.NotNull(factory.Subscriptions[_typeMetadata]);
        }

        [Fact]
        public void GetConsumerBindingForNullTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            Assert.Throws<ArgumentNullException>(() => factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, null));
        }

        [Fact]
        public void GetConsumerBindingForMultidimensionalTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, 2, new int[] { 21, 37 });
            Assert.Throws<ValueRankOutOfRangeException>(() => factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo));
        }

        [Fact]
        public void GetConsumerBindingForNotSupportedTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.NodeId, -2, new int[] { });
            Assert.Throws<UnsupportedTypeException>(() => factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo));
        }

        [Fact]
        public void SubscribeNotBoundTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            Assert.Throws<UnsupportedTypeException>(() => factory.Subscribe(_typeMetadata, (sender, args) => { }));
        }

        [Fact]
        public void SubscribeBoundTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, -1, new int[] { });
            IConsumerBinding binding = factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo);
            ISubscription subscription = factory.Subscribe(_typeMetadata, (sender, args) => { });
            Assert.NotNull(subscription);
            Assert.Equal(_typeMetadata, subscription.UaTypeMetadata);
            Assert.Equal(binding, subscription.Value);
        }

        [Fact]
        public void SubscriptionPropertyChangedEventHandlerTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory();
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, -1, new int[] { });
            IConsumerBinding binding = factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo);
            bool invoked = false;
            ISubscription subscription = factory.Subscribe(_typeMetadata, (sender, args) => invoked = true);
            subscription.Value = null;
            Assert.True(invoked);
        }
    }
}
