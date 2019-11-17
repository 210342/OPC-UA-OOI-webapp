using M2MCommunication.Core;
using M2MCommunication.Uaooi;
using System;
using System.Reflection;
using UAOOI.Configuration.Networking.Serialization;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class SubscriptionTest
    {
        private static readonly UaTypeMetadata _typMetadata = new UaTypeMetadata("repo", "type");

        [Fact]
        public void ConstructorTest()
        {
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.DateTime);
            Subscription subscription = new Subscription(typeInfo, _typMetadata, DateTime.MinValue);
            Assert.NotNull(subscription.TypeInfo);
            Assert.NotNull(subscription.Value);
            Assert.NotNull(subscription.UaTypeMetadata);
            Assert.Equal(_typMetadata, subscription.UaTypeMetadata);
            Assert.Equal(DateTime.MinValue, subscription.Value);
            Assert.Equal(typeInfo, subscription.TypeInfo);
        }

        [Fact]
        public void ConstructorTypeNameNullTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Subscription(new UATypeInfo(BuiltInType.DateTime), null, DateTime.MinValue));
        }

        [Fact]
        public void EnableTest()
        {
            bool raised = false;
            Subscription subscription = new Subscription(new UATypeInfo(BuiltInType.DateTime), _typMetadata, DateTime.MinValue);
            subscription.Enable((sender, args) => raised = true);
            subscription.Value = DateTime.UtcNow;
            Assert.True(raised);
        }

        [Fact]
        public void DisableTest()
        {
            Subscription subscription = new Subscription(new UATypeInfo(BuiltInType.DateTime), _typMetadata, DateTime.MinValue);
            subscription.Disable();
            Assert.Null(subscription
                .GetType()
                .GetEvent("ValueUpdated", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetRaiseMethod());
        }
    }
}
