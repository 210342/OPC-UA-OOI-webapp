using InterfaceModel.Model;
using MessageParsing;
using MessageParsingUnitTest.Mocks;
using System.Collections.Generic;
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
            using (ImageMessageParser sut = new ImageMessageParser(GetTestConfigurationService(), GetTestSubscriptionService(), new TestImageTemplateRepository()))
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
            using (ImageMessageParser sut = new ImageMessageParser(GetTestConfigurationService(), GetTestSubscriptionService(), new TestImageTemplateRepository()))
            {
                await sut.InitialiseAsync(async () => await Task.Run(() => { }));
                Assert.NotNull(sut.ImageTemplates);
                Assert.NotEmpty(sut.ImageTemplates);
                foreach (var template in sut.ImageTemplates)
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
