using InterfaceModel.Model;
using MessageParsingUnitTest.Mocks;
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
                new PropertyTemplate("Name", null, "red")
            );
            IProperty drawable = (printable as PrintableProperty).MapToDrawable(new Point(10, 100));
            Assert.True(drawable is DrawableProperty);
            Assert.Equal(printable.Template.Name, drawable.Template.Name);
            Assert.Equal(printable.Subscription, drawable.Subscription);
            Assert.Equal(printable.Template.HexColor, drawable.Template.HexColor);
            Assert.Equal(10, drawable.Template.Location.X);
            Assert.Equal(100, drawable.Template.Location.Y);
        }

        [Fact]
        public void MapDrawableToPrintableProperty()
        {
            IProperty drawable = new DrawableProperty(
                new TestSubscription() { Value = "Drawable value" },
                new PropertyTemplate("Name", new Point(0, 0), "red")
            );
            IProperty printable = (drawable as DrawableProperty).MapToPrintable();
            Assert.True(printable is PrintableProperty);
            Assert.Equal(drawable.Template.Name, printable.Template.Name);
            Assert.Equal(drawable.Subscription, printable.Subscription);
            Assert.Equal(drawable.Template.HexColor, printable.Template.HexColor);
            Assert.Null(printable.Template.Location);
        }
    }
}
