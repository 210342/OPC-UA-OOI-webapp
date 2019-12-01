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
                Assert.NotNull(sut.GetType().GetProperty("MessageBus", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
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
                Assert.Empty(sut.ImageTemplates);
                foreach (InterfaceModel.Model.ImageTemplate template in sut.ImageTemplates.Values)
                {
                    Assert.NotNull(template.PropertyTemplates);
                    Assert.NotEmpty(template.PropertyTemplates);
                    Assert.NotNull(template.Properties);
                    Assert.Empty(template.Properties);
                    Assert.Empty(template.DrawableProperties);
                    Assert.Empty(template.PrintableProperties);
                }
                Assert.Empty(sut.PrintableProperties);
            }
        }
    }
}
