using M2M_Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;

namespace M2M_CommunicationUnitTest
{
    public class MessageBusTest
    {
        public static IEnumerable<object[]> Messages =>
            new List<object[]>
            {
                new object[] {new MessageTest.TestMessageType(), new MessageTest.TestMessageType(), new Message() },
                new object[] {new MessageTest.TestMessageType(), new Message(), new Message() },
            };

        [Theory]
        [MemberData(nameof(Messages))]
        public void SendMessageTest(IMessage message1, IMessage message2, IMessage message3)
        {
            IMessageBus bus = new MessageBus();
            bus.SendMessage(message1);
            Assert.Single(bus.ReadOldMessages());
            Assert.Single(bus.ReadOldMessagesByType(message1.GetType()));
            Assert.Single(bus.ReadOldMessagesByTypeGuid(message1.TypeGuid));
            Assert.Empty(bus.ReadOldMessagesByType(message3.GetType()));
            bus.SendMessage(message2);
            Assert.Equal(2, bus.ReadOldMessages().Count());
        }

        [Fact]
        public void NewMessageInvocationTest()
        {
            bool invoked = false;
            IMessageBus bus = new MessageBus();
            bus.Subscribe(Message.StaticTypeGuid, message => invoked = true);
            bus.SendMessage(new Message());
            Assert.True(invoked);
        }
    }
}
