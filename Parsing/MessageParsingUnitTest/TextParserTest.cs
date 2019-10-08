using System;
using System.Collections.Generic;
using System.Text;
using MessageParsing;
using Xunit;

namespace MessageParsingUnitTest
{
    public class TextParserTest
    {
        [Fact]
        public void ParseNullArgumenttest()
        {
            IMessageParser parser = new TextMessageParser(null);
            Assert.Throws<ArgumentNullException>(() => parser.Parse(null));
        }

        [Fact]  
        public void ParseAsyncNullArgumentTest()
        {
            IMessageParser parser = new TextMessageParser(null);
            Assert.ThrowsAsync<ArgumentNullException>(async () => await parser.ParseAsync(null));
        }
    }
}
