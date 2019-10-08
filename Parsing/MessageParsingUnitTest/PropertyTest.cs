using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MessageParsing;
using Xunit;

namespace MessageParsingUnitTest
{
    public class PropertyTest
    {
        [Fact]
        public void MapPrintableToDrawableProperty()
        {
            IProperty printable = new PrintableProperty("Printable", "Printable value", Color.White);
            IProperty drawable = (printable as PrintableProperty).MapToDrawable(10, 100, Color.Brown);
            Assert.True(drawable is DrawableProperty);
            Assert.Equal(printable.Name, drawable.Name);
            Assert.Equal(printable.Value, drawable.Value);
            Assert.Equal(printable.FontColor, drawable.FontColor);
            Assert.Equal(10M, drawable.XPosition);
            Assert.Equal(100M, drawable.YPosition);
            Assert.Equal(Color.Brown, drawable.BackgroundColor);
        }

        [Fact]
        public void MapDrawableToPrintableProperty()
        {
            IProperty drawable = new DrawableProperty("Printable", "Printable value", 10, 20, Color.White, Color.Brown);
            IProperty printable = (drawable as DrawableProperty).MapToPrintable();
            Assert.True(printable is PrintableProperty);
            Assert.Equal(drawable.Name, printable.Name);
            Assert.Equal(drawable.Value, printable.Value);
            Assert.Equal(drawable.FontColor, printable.FontColor);
            Assert.Null(printable.XPosition);
            Assert.Null(printable.YPosition);
            Assert.Null(printable.BackgroundColor);
        }
    }
}
