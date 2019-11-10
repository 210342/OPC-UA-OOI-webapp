using InterfaceModel.Model;
using MessageParsing;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace MessageParsingUnitTest
{
    public class TextMessageParserTest : MessageParserTest
    {
        [Fact]
        public void TextConstructorTest()
        {
            using (TextMessageParser sut = new TextMessageParser(GetTestConfigurationService(), GetTestSubscriptionService()))
            {
                Assert.NotNull(sut.GetType().GetProperty("Configuration", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.GetType().GetProperty("SubscriptionFactory", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.Empty(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut) as IEnumerable<IProperty>);
            }
        }

        [Fact]
        public void InitialiseTest()
        {
            using (TextMessageParser sut = new TextMessageParser(GetTestConfigurationService(), GetTestSubscriptionService()))
            {
                sut.Initialise(async () => await Task.Run(() => { }));
                Assert.NotNull(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotEmpty(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut) as IEnumerable<IProperty>);
                Assert.Empty(sut.DrawableProperties);
                Assert.NotEmpty(sut.PrintableProperties);
            }
        }

        [Fact]
        public async Task InitialiseAsyncTest()
        {
            using (TextMessageParser sut = new TextMessageParser(GetTestConfigurationService(), GetTestSubscriptionService()))
            {
                await sut.InitialiseAsync(async () => await Task.Run(() => { }));
                Assert.NotNull(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotEmpty(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut) as IEnumerable<IProperty>);
                Assert.Empty(sut.DrawableProperties);
                Assert.NotEmpty(sut.PrintableProperties);
            }
        }
    }
}
