using M2MCommunication.Uaooi;
using System;
using System.Reflection;
using UAOOI.Configuration.Networking.Serialization;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class SubscriptionTest
    {
        private static readonly string _typeName = "the name of the type";

        [Fact]
        public void ConstructorTest()
        {
            UATypeInfo typeInfo = new UATypeInfo(BuiltInType.DateTime);
            Subscription subscription = new Subscription(typeInfo, _typeName, DateTime.MinValue);
            Assert.NotNull(subscription.TypeInfo);
            Assert.NotNull(subscription.Value);
            Assert.NotNull(subscription.TypeName);
            Assert.Equal(_typeName, subscription.TypeName);
            Assert.Equal(DateTime.MinValue, subscription.Value);
            Assert.Equal(typeInfo, subscription.TypeInfo);
        }

        [Fact]
        public void ConstructorTypeNameNullTest()
        {
            Assert.Throws<ArgumentException>(() => new Subscription(new UATypeInfo(BuiltInType.DateTime), null, DateTime.MinValue));
        }

        [Fact]
        public void ConstructorTypeNameWhiteSpaceTest()
        {
            Assert.Throws<ArgumentException>(() => new Subscription(new UATypeInfo(BuiltInType.DateTime), $"{Environment.NewLine}", DateTime.MinValue));
        }

        [Fact]
        public void EnableTest()
        {
            bool raised = false;
            Subscription subscription = new Subscription(new UATypeInfo(BuiltInType.DateTime), _typeName, DateTime.MinValue);
            subscription.Enable((sender, args) => raised = true);
            subscription.Value = DateTime.UtcNow;
            Assert.True(raised);
        }

        [Fact]
        public void DisableTest()
        {
            Subscription subscription = new Subscription(new UATypeInfo(BuiltInType.DateTime), _typeName, DateTime.MinValue);
            subscription.Disable();
            Assert.Null(subscription
                .GetType()
                .GetEvent("ValueUpdated", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetRaiseMethod());
        }
    }
}
