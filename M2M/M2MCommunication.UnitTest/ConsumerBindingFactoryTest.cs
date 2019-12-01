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
    }
}
