﻿using MessageParsing.Model;
using System.Drawing;
using Xunit;

namespace MessageParsingUnitTest
{
    public class PropertyTest
    {
        [Fact]
        public void MapPrintableToDrawableProperty()
        {
            IProperty printable = new PrintableProperty(
                new TestSubscription() { Value = "Printable value" },
                new PropertyTemplate("Name", null, Color.Red, null)
            );
            IProperty drawable = (printable as PrintableProperty).MapToDrawable(new Point(10, 100), Color.Brown);
            Assert.True(drawable is DrawableProperty);
            Assert.Equal(printable.Template.Name, drawable.Template.Name);
            Assert.Equal(printable.Subscription, drawable.Subscription);
            Assert.Equal(printable.Template.FontColor, drawable.Template.FontColor);
            Assert.Equal(10M, drawable.Template.Location.Value.X);
            Assert.Equal(100M, drawable.Template.Location.Value.Y);
            Assert.Equal(Color.Brown, drawable.Template.BackgroundColor);
        }

        [Fact]
        public void MapDrawableToPrintableProperty()
        {
            IProperty drawable = new DrawableProperty(
                new TestSubscription() { Value = "Drawable value" },
                new PropertyTemplate("Name", new Point(0, 0), Color.Red, Color.White)
            );
            IProperty printable = (drawable as DrawableProperty).MapToPrintable();
            Assert.True(printable is PrintableProperty);
            Assert.Equal(drawable.Template.Name, printable.Template.Name);
            Assert.Equal(drawable.Subscription, printable.Subscription);
            Assert.Equal(drawable.Template.FontColor, printable.Template.FontColor);
            Assert.Null(printable.Template.Location);
            Assert.Null(printable.Template.BackgroundColor);
        }
    }
}
