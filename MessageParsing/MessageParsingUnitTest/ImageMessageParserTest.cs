﻿using InterfaceModel.Model;
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
                Assert.NotNull(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.Empty(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut) as IEnumerable<IProperty>);
            }
        }

        [Fact]
        public void InitialiseTest()
        {
            using (ImageMessageParser sut = new ImageMessageParser(GetTestConfigurationService(), GetTestSubscriptionService(), new TestImageTemplateRepository()))
            {
                sut.Initialise(async () => await Task.Run(() => { }));
                Assert.NotNull(sut.ImageTemplate);
                Assert.NotNull(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotEmpty(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut) as IEnumerable<IProperty>);
                Assert.NotEmpty(sut.DrawableProperties);
                Assert.NotEmpty(sut.PrintableProperties);
            }
        }

        [Fact]
        public async Task InitialiseAsyncTest()
        {
            using (ImageMessageParser sut = new ImageMessageParser(GetTestConfigurationService(), GetTestSubscriptionService(), new TestImageTemplateRepository()))
            {
                await sut.InitialiseAsync(async () => await Task.Run(() => { }));
                Assert.NotNull(sut.ImageTemplate);
                Assert.NotNull(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut));
                Assert.NotEmpty(sut.GetType().GetProperty("Properties", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(sut) as IEnumerable<IProperty>);
                Assert.NotEmpty(sut.DrawableProperties);
                Assert.NotEmpty(sut.PrintableProperties);
            }
        }
    }
}
