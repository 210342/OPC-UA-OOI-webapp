using M2MCommunication;
using System;
using Xunit;

namespace M2MCommunicationUnitTest
{
    public class MessageTest
    {
        internal class TestMessageType : IMessage
        {
            public static Guid StaticTypeGuid => new Guid("052AD04E-5E3C-41A6-88B6-AFD879C31F33");
            public Guid Id => Guid.NewGuid();
            public Guid TypeGuid { get => StaticTypeGuid; }
            public string TypeAsString => TypeGuid.ToString();
            public string Content => "Test Content";
            public DateTime TimeSent => DateTime.UtcNow;
            public int Size => 256;
        }

        [Fact]
        public void StaticTypeGuidTest()
        {
            Assert.Equal(Message.StaticTypeGuid, Message.StaticTypeGuid);
        }

        [Fact]
        public void TypeGuidTest()
        {
            IMessage message1 = new Message();
            IMessage message2 = new Message();
            Assert.Equal(message1.TypeGuid, message2.TypeGuid);
        }

        [Fact]
        public void IdTest()
        {
            IMessage message1 = new Message();
            IMessage message2 = new Message();
            Assert.NotEqual(message1.Id, message2.Id);
        }

        [Fact]
        public void HashCodeWithSameTypeTest()
        {
            IMessage message1 = new Message();
            IMessage message2 = new Message();
            Assert.NotEqual(message1.GetHashCode(), message2.GetHashCode());
        }

        [Fact]
        public void EqualsWithSameTypeTest()
        {
            IMessage message1 = new Message();
            IMessage message2 = new Message();
            Assert.NotEqual(message1, message2);
        }

        [Fact]
        public void TypeGuidWithDifferentTypeTest()
        {
            IMessage message1 = new Message();
            IMessage message2 = new TestMessageType();
            Assert.NotEqual(message1.TypeGuid, message2.TypeGuid);
        }

        [Fact]
        public void StaticTypeGuidWithDifferentTypeTest()
        {
            Assert.NotEqual(Message.StaticTypeGuid, TestMessageType.StaticTypeGuid);
        }

        [Fact]
        public void HashCodeWithDifferentTypeTest()
        {
            IMessage message1 = new Message();
            IMessage message2 = new TestMessageType();
            Assert.NotEqual(message1.GetHashCode(), message2.GetHashCode());
        }

        [Fact]
        public void EqualsWithDifferentTypeTest()
        {
            IMessage message1 = new Message();
            IMessage message2 = new TestMessageType();
            Assert.NotEqual(message1, message2);
        }
    }
}
