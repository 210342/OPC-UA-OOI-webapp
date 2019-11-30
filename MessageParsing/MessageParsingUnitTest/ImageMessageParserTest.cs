using MessageParsing;
using MessageParsingUnitTest.Mocks;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace MessageParsingUnitTest
{
    public class ImageMessageParserTest : MessageParserTest
    {

        [Fact]
        public void ImageConstructorTest()
        {
            using (ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), Settings, new TestImageTemplateRepository()))
            {
                Assert.NotNull(sut.GetType().GetProperty("Configuration", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.GetType().GetProperty("SubscriptionFactory", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.GetType().GetProperty("ImageTemplateRepository", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotNull(sut.ImageTemplates);
                Assert.Empty(sut.ImageTemplates);
            }
        }

        [Fact]
        public async Task InitialiseAsyncTest()
        {
            using (ImageMessageParser sut = new ImageMessageParser(GetMessageBusService(), Settings, new TestImageTemplateRepository()))
            {
                await sut.InitialiseAsync(async () => await Task.Run(() => { }));
                Assert.NotNull(sut.ImageTemplates);
                Assert.NotEmpty(sut.ImageTemplates);
                foreach (InterfaceModel.Model.ImageTemplate template in sut.ImageTemplates.Values)
                {
                    Assert.NotNull(template.Properties);
                    Assert.NotEmpty(template.Properties);
                    Assert.NotEmpty(template.DrawableProperties);
                    Assert.NotEmpty(template.PrintableProperties);
                }
                Assert.NotEmpty(sut.PrintableProperties);
            }
        }
    }
}
