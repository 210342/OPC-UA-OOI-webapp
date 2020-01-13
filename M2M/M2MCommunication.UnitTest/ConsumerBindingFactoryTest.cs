using M2MCommunication.Core.CommonTypes;
using M2MCommunication.Core.Exceptions;
using M2MCommunication.Core.Interfaces;
using M2MCommunication.UaooiInjections.Components;
using System;
using UAOOI.Configuration.Networking.Serialization;
using UAOOI.Networking.SemanticData.DataRepository;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class ConsumerBindingFactoryTest
    {
        private static readonly UaTypeMetadata _typeMetadata = new UaTypeMetadata("repository group", "type name");
        private static readonly IConfiguration _configuration = new Configuration(null, string.Empty);
        private static readonly IConsumerViewModel _consumerViewModel = new TestConsumerViewModel();

        private class TestConsumerViewModel : IConsumerViewModel
        {
            public void AddSubscription(ISubscription subscription)
            {
            }
        }

        [Fact]
        public void ConstructorTest()
        {
            ConsumerBindingFactory sut = new ConsumerBindingFactory(null, _configuration);
            Assert.Null(sut.GetType()
                .GetField("_logger", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .GetValue(sut));
            Assert.NotNull(sut.GetType()
                .GetField("_configuration", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .GetValue(sut));
        }

        [Fact]
        public void ConstructorNullConfigurationTest()
        {
            Assert.Throws<ComponentNotInitialisedException>(() => new ConsumerBindingFactory(null, null));
        }

        [Fact]
        public void GetProducerBindingTest()
        {
            Assert.Throws<NotSupportedException>(() =>
                new ConsumerBindingFactory(null, _configuration).GetProducerBinding(string.Empty, string.Empty, null));
        }

        [Fact]
        public void GetConsumerBindingTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory(null, _configuration);
            factory.Initialise(_consumerViewModel);
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, -1, new int[] { });
            IConsumerBinding binding = factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo);
            Assert.NotNull(binding);
        }

        [Fact]
        public void GetConsumerBindingForNullTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory(null, _configuration);
            Assert.Throws<ArgumentNullException>(() => factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, null));
        }

        [Fact]
        public void GetConsumerBindingForMultidimensionalTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory(null, _configuration);
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.Byte, 2, new int[] { 21, 37 });
            Assert.Throws<ValueRankOutOfRangeException>(() => factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo));
        }

        [Fact]
        public void GetConsumerBindingForNotSupportedTypeTest()
        {
            ConsumerBindingFactory factory = new ConsumerBindingFactory(null, _configuration);
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.NodeId, -2, new int[] { });
            Assert.Throws<UnsupportedTypeException>(() => factory.GetConsumerBinding(_typeMetadata.RepositoryGroupName, _typeMetadata.TypeName, typeInfo));
        }
    }
}
