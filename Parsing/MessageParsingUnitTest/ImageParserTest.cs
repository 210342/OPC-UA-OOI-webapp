using System;
using System.Collections.Generic;
using System.Text;
using MessageParsing;
using Xunit;

namespace MessageParsingUnitTest
{
    public class ImageParserTest
    {
        [Fact]
        public void ParseNullArgumentTest()
        {
            IMessageParser parser = new ImageMessageParser(null, null);
            Assert.Throws<ArgumentNullException>(() => parser.Parse(null));
        }

        [Fact]
        public void ParseAsyncNullArgumentTest()
        {
            IMessageParser parser = new ImageMessageParser(null, null);
            Assert.ThrowsAsync<ArgumentNullException>(async () => await parser.ParseAsync(null));
        }
    }
}
